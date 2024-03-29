﻿namespace AnalyzingTwitter;

public class Domain
{
    public string description { get; set; }
    public string id { get; set; }
    public string name { get; set; }
}

public class Entity
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Hashtag
{
    public int end { get; set; }
    public int start { get; set; }
    public string tag { get; set; }
}

public class Url
{
    public string display_url { get; set; }
    public int end { get; set; }
    public string expanded_url { get; set; }
    public int start { get; set; }
    public string url { get; set; }
    public int? status { get; set; }
    public string unwound_url { get; set; }
}

public class Annotation
{
    public int end { get; set; }
    public string normalized_text { get; set; }
    public double probability { get; set; }
    public int start { get; set; }
    public string type { get; set; }
}

public class Mention
{
    public int end { get; set; }
    public string id { get; set; }
    public int start { get; set; }
    public string username { get; set; }
}

public class Entities
{
    public IList<Hashtag> hashtags { get; set; }
    public IList<Url> urls { get; set; }
    public IList<Annotation> annotations { get; set; }
    public IList<Mention> mentions { get; set; }
}

public class PublicMetrics
{
    public int like_count { get; set; }
    public int quote_count { get; set; }
    public int reply_count { get; set; }
    public int retweet_count { get; set; }
    public int followers_count { get; set; }
    public int following_count { get; set; }
    public int listed_count { get; set; }
    public int tweet_count { get; set; }
}

public class ReferencedTweet
{
    public string id { get; set; }
    public string type { get; set; }
}

public class Geo
{
    public string place_id { get; set; }
}

public class Attachments
{
    public IList<string> media_keys { get; set; }
}

public class Tweet
{
    public string author_id { get; set; }
    public IList<ContextAnnotation> context_annotations { get; set; }
    public string conversation_id { get; set; }
    public DateTime created_at { get; set; }
    public Entities entities { get; set; }
    public string id { get; set; }
    public string lang { get; set; }
    public bool possibly_sensitive { get; set; }
    public PublicMetrics public_metrics { get; set; }
    public string reply_settings { get; set; }
    public string source { get; set; }
    public string text { get; set; }
    public Attachments attachments { get; set; }
    public IList<ReferencedTweet> referenced_tweets { get; set; }
    public string in_reply_to_user_id { get; set; }
    public Geo geo { get; set; }
}

public class Error
{
    public string detail { get; set; }
    public string parameter { get; set; }
    public string resource_id { get; set; }
    public string resource_type { get; set; }
    public string title { get; set; }
    public string type { get; set; }
    public string value { get; set; }
    public string section { get; set; }
}

public class Medium
{
    public int height { get; set; }
    public string media_key { get; set; }
    public string preview_image_url { get; set; }
    public string type { get; set; }
    public int width { get; set; }
    public string url { get; set; }
    public int? duration_ms { get; set; }
    public PublicMetrics public_metrics { get; set; }
}

public class Place
{
    public string country { get; set; }
    public string country_code { get; set; }
    public string full_name { get; set; }
    public Geo geo { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string place_type { get; set; }
}

public class ContextAnnotation
{
    public Domain domain { get; set; }
    public Entity entity { get; set; }
}

public class User
{
    public DateTime created_at { get; set; }
    public string description { get; set; }
    public Entities entities { get; set; }
    public string id { get; set; }
    public string location { get; set; }
    public string name { get; set; }

    public string profile_image_url { get; set; }
    //public bool protected { get; set; }

    public PublicMetrics public_metrics { get; set; }
    public string url { get; set; }
    public string username { get; set; }
    public bool verified { get; set; }
    public string pinned_tweet_id { get; set; }
}

public class Includes
{
    public IList<Medium> media { get; set; }
    public IList<Place> places { get; set; }
    public IList<Tweet> tweets { get; set; }
    public IList<User> users { get; set; }
}

public class Meta
{
    public string newest_id { get; set; }
    public string next_token { get; set; }
    public string oldest_id { get; set; }
    public int result_count { get; set; }
}

public class TwitterData
{
    public IList<Tweet> data { get; set; }
    public IList<Error> errors { get; set; }
    public Includes includes { get; set; }
    public Meta meta { get; set; }
}