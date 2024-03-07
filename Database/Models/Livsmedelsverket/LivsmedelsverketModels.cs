namespace Database.Models.Livsmedelsverket;

public class Link
{
    public string href { get; set; }
    public string rel { get; set; }
    public string method { get; set; }
}

public class Link2
{
    public string href { get; set; }
    public string rel { get; set; }
    public string method { get; set; }
}

public class Livsmedel
{
    public int livsmedelsTypId { get; set; }
    public string livsmedelsTyp { get; set; }
    public int nummer { get; set; }
    public DateTime version { get; set; }
    public string namn { get; set; }
    public string vetenskapligtNamn { get; set; }
    public string projekt { get; set; }
    public List<Link> links { get; set; }
    public string tillagningsmetod { get; set; }
    public string analys { get; set; }
}

public class Meta
{
    public int totalRecords { get; set; }
    public int offset { get; set; }
    public int limit { get; set; }
    public int count { get; set; }
}

public class Root
{
    public Meta _meta { get; set; }
    public List<Link> _links { get; set; }
    public List<Livsmedel> livsmedel { get; set; }
}

