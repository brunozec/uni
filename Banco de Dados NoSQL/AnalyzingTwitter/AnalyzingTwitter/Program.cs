// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Text;
using AnalyzingTwitter;
using Neo4j.Driver;


/*
 PRÁTICA 01- JSON COM NEO4J.
QUESTÃO 01: DESCOBERTA DA HASHTAG PRINCIPAL
Sua missão nesta prática será encontrar qual hashtag foi usada para gerar todos os arquivos
JSON. Para isso, descubra qual delas está presente em todos os tweets coletados. Confirme sua
descoberta com a geração de um grafo que mostre um nó central tendo esta hashtag e os demais
nós como tweets que contém esta hashtag.
Utilize apenas 1 dia de mensagens ou menos nesta representação, desde que atinja o mínimo
de 10 nós contando com o central, para evitar um grafo muito poluído. Caso o tempo de 1 dia seja
muito grande, reduza-o até conseguir um grafo que seja visível.
Cole o grafo gerado em seu caderno de respostas, na área da direita e cole uma imagem de
seu código/query usado para gerar o grafo.
Não esqueça de colocar seu RU tanto no código quanto no grafo, antes de salvar seu arquivo
em PDF.
Por fim, responda à pergunta: Qual foi a hashtag usada como filtro para coleta dos dados
analisados?
Para auxílio, seguem alguns links interessantes:
• https://neo4j.com/blog/twitter-hashtag-impact-neo4j-python-javascript/
• https://developer.twitter.com/en/docs/twitter-api/data-dictionary/example-payloads
 */

/*
 PROFILE match (t:Tweet)-[:USES]->(h:Hashtag)
with h, count(t) as t_count
order by t_count desc
with collect(h)[0..1] as h_list, t_count
unwind h_list as h
optional match (t)-[:USES]->(h)
return {hashtag: h, count: t_count,tweets: collect(t)[0..50]}
 */

/*
 QUESTÃO 02: ANÁLISE DOS DADOS SEGUNDO VIÉS A SUA
ESCOLHA
Após a primeira questão, você já deve saber como criar um grafo a partir dos dados fornecidos,
mas o maior desafio é conseguir extrair informações úteis de grandes quantidades de dados que
podem parecer desconexos.
Sua tarefa aqui é analisar os dados do banco e definir qual informação você gostaria de obter
e que tenha potencial de gerar um grafo com mais de 10 nós interligados entre si.
Com base em sua escolha, defina um período para realizar sua análise que seja grande o
suficiente para apresentar os resultados que você deseja, porém que não criem um grafo ilegível.
A sugestão é criar um grafo com mais de 10 nós e menos de 200.
Então, crie a consulta ao banco de dados que apresente os dados que você escolheu analisar
e gere o grafo de sua consulta para colocação no seu caderno de respostas.
Por fim, responda à pergunta: Qual foi o comando usado por você para dar entrada dos dados
em JSON no seu banco de dados Neo4j?
 */


//52111 tweets
Console.WriteLine("Hello, World!");


var json_path = @"C:\Users\bruno\Downloads\JSON_31DEZ2019a01ABR2020\";

var files = System.IO.Directory.GetFiles(json_path);

IList<TwitterData> data = new List<TwitterData>();
IList<Tweet> tweets = new List<Tweet>();
IList<Tweet> tweets_data = new List<Tweet>();
IList<User> users = new List<User>();
IList<Place> places = new List<Place>();

foreach (var file in files)
{
    try
    {
        var lines = System.IO.File.ReadAllText(file);

        var d = System.Text.Json.JsonSerializer.Deserialize<TwitterData>(lines);

        data.Add(d);
    }
    catch (Exception e)
    {
    }
}

foreach (var twitterData in data)
{
    foreach (var tweet in twitterData?.data ?? new List<Tweet>())
    {
        tweets_data.Add(tweet);
    }

    foreach (var user in twitterData?.includes?.users ?? new List<User>())
    {
        if (!users.Any(u => u.id == user.id))
            users.Add(user);
    }

    foreach (var place in twitterData?.includes?.places ?? new List<Place>())
    {
        if (!places.Any(u => u.id == place.id))
            places.Add(place);
    }

    foreach (var tweet in twitterData?.includes?.tweets ?? new List<Tweet>())
    {
        tweets.Add(tweet);
    }
}

var unico_data = tweets_data.Where(i => !tweets.Any(j => j.id == i.id));
var unico_tweets = tweets.Where(i => !tweets_data.Any(j => j.id == i.id));

// Aura queries use an encrypted connection using the "neo4j+s" protocol

var uri = "bolt://localhost:7687";

var neo4jUser = "neo4j";

var password = "1AEblnHE5Dg28rWiMHnUEHOe24fGq-vmfEo7FL12dg8";

bool _disposed = false;

IDriver driver = GraphDatabase.Driver(uri, AuthTokens.Basic(neo4jUser, password));

var constratin1 = "CREATE CONSTRAINT ON (u:Tweet) ASSERT u.id IS UNIQUE";
var constratin2 = "CREATE CONSTRAINT ON (u:Hashtag) ASSERT u.tag IS UNIQUE";
var constratin3 = "CREATE CONSTRAINT ON (u:Author) ASSERT u.id IS UNIQUE";

List<Hashtag> hashtags = new List<Hashtag>();
List<(string Hashtag, IList<Tweet> tweets)> hashtags_tweets = new List<(string Hashtag, IList<Tweet> tweets)>();


foreach (var tweet in tweets_data)
{
    foreach (var hashtag in tweet.entities?.hashtags ?? new List<Hashtag>())
    {
        if (!hashtags.Any(i => RemoveDiacritics(i.tag.Trim().ToUpper()) == RemoveDiacritics(hashtag.tag.Trim().ToUpper())))
            hashtags.Add(new Hashtag
            {
                tag = hashtag.tag.Trim().ToUpper()
            });
    }
}

foreach (var hashtag in hashtags)
{
    hashtags_tweets.Add((RemoveDiacritics(hashtag.tag.Trim().ToUpper())
      , tweets_data.Where(i => (i.entities?.hashtags ?? new List<Hashtag>()).Any(j => RemoveDiacritics(j.tag.Trim().ToUpper()) == RemoveDiacritics(hashtag.tag.Trim().ToUpper()))).ToList()));
}

var ordered = hashtags_tweets.OrderByDescending(i => i.tweets.Count);
foreach (var ht in ordered)
{
    Console.WriteLine($"{RemoveDiacritics(ht.Hashtag.Trim().ToUpper())} {ht.tweets.Count} tweets");
}

//1977 hastags
//52111 tweets

string RemoveDiacritics(string text)
{
    if (string.IsNullOrWhiteSpace(text))
        return text;

    text = text.Normalize(NormalizationForm.FormD);
    var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
    return new string(chars).Normalize(NormalizationForm.FormC).Trim().ToUpper();
}

try
{
    await using var session = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
    var res1 = await session.RunAsync(constratin1);
}
catch (Exception e)
{
}

try
{
    await using var session = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
    var res2 = await session.RunAsync(constratin2);
}
catch (Exception e)
{
}

try
{
    await using var session = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
    var res2 = await session.RunAsync(constratin3);
}
catch (Exception e)
{
}

var tweetssemglobo=tweets_data.Where(where => !where.entities.hashtags.Any(hashtag => RemoveDiacritics(hashtag.tag).Contains("globo")));

var _lock = new object();

bool importData = true;
bool importTweetsOnly = false;
bool importAuthor = true;
bool importPlace = false;

try
{
    if (importAuthor)
    {
        await using var session = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
        await Parallel.ForEachAsync(users,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }
          , async (user, token) =>
            {
                try
                {
                    var query
                        = "MERGE (a:Author { id: $id, created_at:$created_at, description:$description, username:$username, url:$url, profile_image_url:$profile_image_url, name: $name, location:$location, followers_count: $followers_count, tweet_count: $tweet_count})";

                    await session.RunAsync(query, new
                    {
                        user.id
                      , user.created_at
                      , user.description
                      , user.username
                      , user.url
                      , user.profile_image_url
                      , user.name
                      , location = user.location ?? "N/A"
                      , user.public_metrics.followers_count
                      , user.public_metrics.tweet_count
                    });
                    Console.WriteLine($"Inserted Author id: {user.id}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Author: {e.Message}");
                }
            });
    }

    if (importPlace)
    {
        await using var session2 = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
        await Parallel.ForEachAsync(places,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }
          , async (place, token) =>
            {
                try
                {
                    var query
                        = "MERGE (p:Place { id: $id, country: $country, name: $name, place_type: $place_type})";

                    await session2.RunAsync(query, new
                    {
                        place.id
                      , place.country
                      , place.full_name
                      , place.name
                      , place.place_type
                    });
                    Console.WriteLine($"Inserted Place id: {place.id}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Place: {e.Message}");
                }
            });
    }

    if (importTweetsOnly)
    {
        await Parallel.ForEachAsync(tweets,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }
          , async (tweet, token) =>
            {
                try
                {
                     await using var session3 = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
                    // var queryTweet
                    //     = "MERGE (t:Tweet { id: $id, author_id: $author_id, text: $text, created_at: $created_at, conversation_id: $conversation_id, like_count: $like_count, reply_count: $reply_count, quote_count: $quote_count, retweet_count: $retweet_count, source: $source, place_id: $place_id })";
                    //
                    // var queryTweet_res1 = await session3.RunAsync(queryTweet, new
                    // {
                    //     tweet.id
                    //   , tweet.author_id
                    //   , tweet.text
                    //   , tweet.created_at
                    //   , tweet.conversation_id
                    //   , like_count = tweet.public_metrics?.like_count ?? 0
                    //   , reply_count = tweet.public_metrics?.reply_count ?? 0
                    //   , quote_count = tweet.public_metrics?.quote_count ?? 0
                    //   , retweet_count = tweet.public_metrics?.retweet_count ?? 0
                    //   , tweet.source
                    //   , place_id = tweet.geo?.place_id ?? "N/A"
                    // });
                    //
                    // Console.WriteLine($"Inserted tweet id: {tweet.id}");
                    //
                    // if (importAuthor)
                    // {
                    //     var queryAuthorRelation = "MATCH (t:Tweet {id: $id}), (a: Author {id: $author_id}) MERGE (t)-[:AUTHOR]->(a)";
                    //
                    //     var queryAuthorRelation_res1 = await session3.RunAsync(queryAuthorRelation, new
                    //     {
                    //         tweet.id
                    //       , tweet.author_id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} author id: {tweet.author_id}");
                    // }

                    // if (!string.IsNullOrEmpty(tweet.conversation_id))
                    // {
                    //     var queryConversationRelation = "MATCH (t:Tweet {id: $id}), (c: Conversation {id: $conversation_id}) MERGE (t)-[:CONVERSATION]->(c)";
                    //
                    //     var queryConversationRelation_res1 = await session3.RunAsync(queryConversationRelation, new
                    //     {
                    //         tweet.id
                    //       , tweet.conversation_id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} conversation_id: {tweet.conversation_id}");
                    // }

                    // foreach (var hashtag in tweet.entities?.hashtags ?? new List<Hashtag>())
                    // {
                    //     var queryHash = "MERGE (h:Hashtag { tag: $tag })";
                    //
                    //     var queryHash_res1 = await session3.RunAsync(queryHash, new
                    //     {
                    //         tag = RemoveDiacritics(hashtag.tag.Trim().ToUpper())
                    //     });
                    //     Console.WriteLine($"Inserted hashtag tag: {hashtag.tag}");
                    //
                    //     var queryHashRelation = "MATCH (t:Tweet {id: $id}), (h: Hashtag {tag: $tag}) MERGE (t)-[:USES]->(h)";
                    //
                    //     var queryHashRelation_res1 = await session3.RunAsync(queryHashRelation, new
                    //     {
                    //         tweet.id
                    //       , tag = RemoveDiacritics(hashtag.tag.Trim().ToUpper())
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} hashtag tag: {hashtag.tag}");
                    // }
                    //
                    // foreach (var mention in tweet.entities?.mentions ?? new List<Mention>())
                    // {
                    //     var queryMention = "MERGE (a:Author { id: $id, username: $username })";
                    //
                    //     var queryMention_res1 = await session3.RunAsync(queryMention, new
                    //     {
                    //         mention.id
                    //       , mention.username
                    //     });
                    //     Console.WriteLine($"Inserted mention id: {mention.id}");
                    //
                    //     var queryMentionRelation = "MATCH (t:Tweet {id: $id}), (m: Mention {id: $mentionId}) MERGE (t)-[:MENTIONS]->(m)";
                    //
                    //     var queryMentionRelation_res1 = await session3.RunAsync(queryMentionRelation, new
                    //     {
                    //         tweet.id
                    //       , mentionId = mention.id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id}  mention id: {mention.id}");
                    // }

                    //foreach (var annotation in tweet.entities?.annotations ?? new List<Annotation>())
                    // {
                    //     var queryAnnotation = "MERGE (a:Annotation { type :$type, normalized_text: $normalized_text, probability:$probability })";
                    //
                    //     var queryAnnotation_res1 = await session3.RunAsync(queryAnnotation, new
                    //     {
                    //         annotation.type
                    //       , annotation.normalized_text
                    //       , annotation.probability
                    //     });
                    //     Console.WriteLine($"Inserted annotation normalized_text: {annotation.normalized_text}");
                    //
                    //     var queryAnnotationRelation = "MATCH (t:Tweet {id: $id}), (a: Annotation {normalized_text: $normalized_text}) MERGE (t)-[:NOTES]->(a)";
                    //
                    //     var queryAnnotationRelation_res1 = await session3.RunAsync(queryAnnotationRelation, new
                    //     {
                    //         tweet.id
                    //       , annotation.normalized_text
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} annotation normalized_text: {annotation.normalized_text}");
                    // }

                    foreach (var referencedTweet in tweet.referenced_tweets ?? new List<ReferencedTweet>())
                    {
                        var queryReferencedRelation = $"MATCH (t:Tweet {{id: $id}}), (r: Tweet {{id: $referenced}}) MERGE (t)-[:{referencedTweet.type.ToUpper()}]->(r)";

                        var queryReferencedRelation_res1 = await session3.RunAsync(queryReferencedRelation, new
                        {
                            tweet.id
                          , referenced = referencedTweet.id
                        });
                        Console.WriteLine($"Inserted relation tweet id: {tweet.id} referenced $id: {referencedTweet.id}");
                    }

                    if (importPlace && !string.IsNullOrEmpty(tweet.geo?.place_id))
                    {
                        var queryGeoRelation = "MATCH (t:Tweet {id: $id}), (p: Place {id: $place_id}) MERGE (t)-[:LOCATION]->(p)";

                        var queryGeoRelation_res1 = await session3.RunAsync(queryGeoRelation, new
                        {
                            tweet.id
                          , tweet.geo.place_id
                        });
                        Console.WriteLine($"Inserted relation tweet id: {tweet.id} place $id: {tweet.geo.place_id}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Tweet: {e.Message}");
                }
            });
    }

    if (importData)
    {
        Console.WriteLine($"Importa data");
        await Parallel.ForEachAsync(tweets_data
          , new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }
          , async (tweet, token) =>
            {
                await using var session4 = driver.AsyncSession(configBuilder => configBuilder.WithDatabase("neo4j"));
                try
                {
                    var queryTweet
                        = "MERGE (t:Tweet { id: $id, author_id: $author_id, text:$text, created_at: $created_at, conversation_id: $conversation_id, like_count: $like_count, reply_count: $reply_count, quote_count: $quote_count, retweet_count: $retweet_count, source: $source })";

                    var queryTweet_res1 = await session4.RunAsync(queryTweet, new
                    {
                        tweet.id
                      , tweet.author_id
                      , tweet.text
                      , tweet.created_at
                      , tweet.conversation_id
                      , like_count = tweet.public_metrics?.like_count ?? 0
                      , reply_count = tweet.public_metrics?.reply_count ?? 0
                      , quote_count = tweet.public_metrics?.quote_count ?? 0
                      , retweet_count = tweet.public_metrics?.retweet_count ?? 0
                      , tweet.source
                      , place_id = tweet.geo?.place_id ?? "N/A"
                    });

                    Console.WriteLine($"Inserted tweet id: {tweet.id}");

                    if (importAuthor)
                    {
                        var queryAuthorRelation = "MATCH (t:Tweet {id: $id}), (a: Author {id: $author_id}) MERGE (a)-[:AUTHOR]->(t)";

                        var queryAuthorRelation_res1 = await session4.RunAsync(queryAuthorRelation, new
                        {
                            tweet.id
                          , tweet.author_id
                        });
                        Console.WriteLine($"Inserted relation tweet id: {tweet.id} author id: {tweet.author_id}");
                    }
                    // if (!string.IsNullOrEmpty(tweet.conversation_id))
                    // {
                    //     var queryConversationRelation = "MATCH (t:Tweet {id: $id}), (c: Conversation {id: $conversation_id}) MERGE (t)-[:CONVERSATION]->(c)";
                    //
                    //     var queryConversationRelation_res1 = await session4.RunAsync(queryConversationRelation, new
                    //     {
                    //         tweet.id
                    //       , tweet.conversation_id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} conversation_id: {tweet.conversation_id}");
                    // }

                    foreach (var hashtag in tweet.entities?.hashtags ?? new List<Hashtag>())
                    {
                        var queryHash = "MERGE (h:Hashtag { tag: $tag })";

                        var queryHash_res1 = await session4.RunAsync(queryHash, new
                        {
                            tag = RemoveDiacritics(hashtag.tag.Trim().ToUpper())
                        });
                        Console.WriteLine($"Inserted hashtag tag: {hashtag.tag}");

                        var queryHashRelation = "MATCH (t:Tweet {id: $id}), (h: Hashtag {tag: $tag}) MERGE (t)-[:USES]->(h)";

                        var queryHashRelation_res1 = await session4.RunAsync(queryHashRelation, new
                        {
                            tweet.id
                          , tag = RemoveDiacritics(hashtag.tag.Trim().ToUpper())
                        });
                        Console.WriteLine($"Inserted relation tweet id: {tweet.id} hashtag tag: {hashtag.tag}");
                    }
                    
                    // foreach (var mention in tweet.entities?.mentions ?? new List<Mention>())
                    // {
                    //     var queryMention = "MERGE (a:Author { id: $id, username: $username })";
                    //
                    //     var queryMention_res1 = await session4.RunAsync(queryMention, new
                    //     {
                    //         mention.id
                    //       , mention.username
                    //     });
                    //     Console.WriteLine($"Inserted mention id: {mention.id}");
                    //
                    //     var queryMentionRelation = "MATCH (t:Tweet {id: $id}), (m: Mention {id: $mentionId}) MERGE (t)-[:MENTIONS]->(m)";
                    //
                    //     var queryMentionRelation_res1 = await session4.RunAsync(queryMentionRelation, new
                    //     {
                    //         tweet.id
                    //       , mentionId = mention.id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id}  mention id: {mention.id}");
                    // }
                    
                    // foreach (var annotation in tweet.entities?.annotations ?? new List<Annotation>())
                    // {
                    //     var queryAnnotation = "MERGE (a:Annotation { type :$type, normalized_text: $normalized_text, probability:$probability })";
                    //
                    //     var queryAnnotation_res1 = await session4.RunAsync(queryAnnotation, new
                    //     {
                    //         annotation.type
                    //       , annotation.normalized_text
                    //       , annotation.probability
                    //     });
                    //     Console.WriteLine($"Inserted annotation normalized_text: {annotation.normalized_text}");
                    //
                    //     var queryAnnotationRelation = "MATCH (t:Tweet {id: $id}), (a: Annotation {normalized_text: $normalized_text}) MERGE (t)-[:NOTES]->(a)";
                    //
                    //     var queryAnnotationRelation_res1 = await session4.RunAsync(queryAnnotationRelation, new
                    //     {
                    //         tweet.id
                    //       , annotation.normalized_text
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} annotation normalized_text: {annotation.normalized_text}");
                    // }

                    foreach (var referencedTweet in tweet.referenced_tweets ?? new List<ReferencedTweet>())
                    {
                        if (tweet.id == referencedTweet.id)
                        {
                            
                        }
                        var queryReferencedRelation = $"MATCH (t:Tweet {{id: $id}}), (r: Tweet {{id: $referenced}}) MERGE (t)-[:{referencedTweet.type.ToUpper()}]->(r)";

                        var queryReferencedRelation_res1 = await session4.RunAsync(queryReferencedRelation, new
                        {
                            tweet.id
                          , referenced = referencedTweet.id
                        });
                        Console.WriteLine($"Inserted relation tweet id: {tweet.id} {referencedTweet.type.ToUpper()} $id: {referencedTweet.id}");
                    }

                    // if (importPlace && !string.IsNullOrEmpty(tweet.geo?.place_id))
                    // {
                    //     var queryGeoRelation = "MATCH (t:Tweet {id: $id}), (p: Place {id: $place_id}) MERGE (t)-[:LOCATION]->(p)";
                    //
                    //     var queryGeoRelation_res1 = await session4.RunAsync(queryGeoRelation, new
                    //     {
                    //         tweet.id
                    //       , tweet.geo.place_id
                    //     });
                    //     Console.WriteLine($"Inserted relation tweet id: {tweet.id} place $id: {tweet.geo.place_id}");
                    // }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Tweet data: {e.Message}");
                }
            });

        Console.WriteLine($"Importa data finished");
    }
}
catch (Exception e)
{
    Console.WriteLine($"Geral: {e.Message}");
}

Console.WriteLine($"finished");
Console.ReadLine();