// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Facet
    {
        public string code { get; set; }
        public string name { get; set; }
        public int priority { get; set; }
        public bool category { get; set; }
        public bool multiSelect { get; set; }
        public bool visible { get; set; }
        public List<TopValue> topValues { get; set; }
        public List<Value> values { get; set; }
    }

    public class Image
    {
        public string imageType { get; set; }
        public string format { get; set; }
        public string url { get; set; }
        public object altText { get; set; }
        public object galleryIndex { get; set; }
        public object width { get; set; }
    }

    public class OriginalImage
    {
        public string imageType { get; set; }
        public string format { get; set; }
        public string url { get; set; }
        public object altText { get; set; }
        public object galleryIndex { get; set; }
        public object width { get; set; }
    }

    public class Pagination
    {
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public object sort { get; set; }
        public int numberOfPages { get; set; }
        public int totalNumberOfResults { get; set; }
        public int allProductsInCategoriesCount { get; set; }
        public int allProductsInSearchCount { get; set; }
    }

    public class PotentialPromotion
    {
        public object threshold { get; set; }
        public string campaignType { get; set; }
        public PromotionTheme promotionTheme { get; set; }
        public int? redeemLimit { get; set; }
        public string savePrice { get; set; }
        public object sorting { get; set; }
        public string textLabelGenerated { get; set; }
        public object textLabelManual { get; set; }
        public string weightVolume { get; set; }
        public double? price { get; set; }
        public object productCodes { get; set; }
        public string promotionType { get; set; }
        public string startDate { get; set; }
        public string conditionLabel { get; set; }
        public string rewardLabel { get; set; }
        public string textLabel { get; set; }
        public string redeemLimitLabel { get; set; }
        public string cartLabel { get; set; }
        public object validUntil { get; set; }
        public object timesUsed { get; set; }
        public int? qualifyingCount { get; set; }
        public List<string> brands { get; set; }
        public object discountPrice { get; set; }
        public string comparePrice { get; set; }
        public string splashTitleText { get; set; }
        public bool realMixAndMatch { get; set; }
        public string mainProductCode { get; set; }
        public int? promotionRedeemLimit { get; set; }
        public object promotionPercentage { get; set; }
        public string endDate { get; set; }
        public string name { get; set; }
        public object priority { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class ProductBasketType
    {
        public string code { get; set; }
        public string type { get; set; }
    }

    public class PromotionTheme
    {
        public string code { get; set; }
        public object visible { get; set; }
    }

    public class Query
    {
        public string url { get; set; }
        public Query query { get; set; }
        public string value { get; set; }
        public List<object> filterQueries { get; set; }
        public object searchQueryContext { get; set; }
        public object searchType { get; set; }
    }

    public class Result
    {
        public bool showGoodPriceIcon { get; set; }
        public object lowestHistoricalPrice { get; set; }
        public string price { get; set; }
        public string manufacturer { get; set; }
        public Image image { get; set; }
        public double ranking { get; set; }
        public List<PotentialPromotion> potentialPromotions { get; set; }
        public ProductBasketType productBasketType { get; set; }
        public double solrSearchScore { get; set; }
        public object energyDeclaration { get; set; }
        public Thumbnail thumbnail { get; set; }
        public OriginalImage originalImage { get; set; }
        public bool online { get; set; }
        public string priceUnit { get; set; }
        public string priceNoUnit { get; set; }
        public string googleAnalyticsCategory { get; set; }
        public string displayVolume { get; set; }
        public string name { get; set; }
        public List<string> labels { get; set; }
        public string title { get; set; }
    }

    public class WillysRoot
    {
        public List<Result> results { get; set; }
        public object sorts { get; set; }
        public Pagination pagination { get; set; }
        public object relatedResults { get; set; }
        public object relatedResultsPagination { get; set; }
        public object currentQuery { get; set; }
        public List<object> breadcrumbs { get; set; }
        public List<Facet> facets { get; set; }
    }

    public class Thumbnail
    {
        public string imageType { get; set; }
        public string format { get; set; }
        public string url { get; set; }
        public object altText { get; set; }
        public object galleryIndex { get; set; }
        public object width { get; set; }
    }

    public class TopValue
    {
        public string code { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public Query query { get; set; }
        public bool selected { get; set; }
    }

    public class Value
    {
        public string code { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public Query query { get; set; }
        public bool selected { get; set; }
    }

