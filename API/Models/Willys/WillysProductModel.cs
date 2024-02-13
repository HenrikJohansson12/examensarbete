namespace API.Models.Willys;

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

    public class LowestHistoricalPrice
    {
        public string currencyIso { get; set; }
        public double value { get; set; }
        public string priceType { get; set; }
        public string formattedValue { get; set; }
        public object minQuantity { get; set; }
        public object maxQuantity { get; set; }
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
        public bool applied { get; set; }
        public LowestHistoricalPrice lowestHistoricalPrice { get; set; }
        public string campaignType { get; set; }
        public PromotionTheme promotionTheme { get; set; }
        public string textLabelGenerated { get; set; }
        public object textLabelManual { get; set; }
        public Price price { get; set; }
        public List<string> productCodes { get; set; }
        public string promotionType { get; set; }
        public string conditionLabel { get; set; }
        public string rewardLabel { get; set; }
        public string textLabel { get; set; }
        public string redeemLimitLabel { get; set; }
        public object cartLabel { get; set; }
        public object validUntil { get; set; }
        public object timesUsed { get; set; }
        public int? qualifyingCount { get; set; }
        public string comparePrice { get; set; }
        public string splashTitleText { get; set; }
        public bool realMixAndMatch { get; set; }
        public string mainProductCode { get; set; }
        public object promotionPercentage { get; set; }
        public string cartLabelFormatted { get; set; }
        public string conditionLabelFormatted { get; set; }
        public object promotionRedeemLimit { get; set; }
        public double? threshold { get; set; }
        public int priority { get; set; }
        public string code { get; set; }
    }

    public class Price
    {
        public string currencyIso { get; set; }
        public double value { get; set; }
        public string priceType { get; set; }
        public string formattedValue { get; set; }
        public object minQuantity { get; set; }
        public object maxQuantity { get; set; }
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
        public List<PotentialPromotion> potentialPromotions { get; set; }
        public double priceValue { get; set; }
        public string price { get; set; }
        public Image image { get; set; }
        public double ranking { get; set; }
        public string depositPrice { get; set; }
        public double? averageWeight { get; set; }
        public string comparePrice { get; set; }
        public string comparePriceUnit { get; set; }
        public bool isDrugProduct { get; set; }
        public double solrSearchScore { get; set; }
        public object energyDeclaration { get; set; }
        public bool newsSplashProduct { get; set; }
        public bool notAllowedB2b { get; set; }
        public bool notAllowedAnonymous { get; set; }
        public string productLine2 { get; set; }
        public string pickupProductLine2 { get; set; }
        public string priceUnit { get; set; }
        public string priceNoUnit { get; set; }
        public double savingsAmount { get; set; }
        public string googleAnalyticsCategory { get; set; }
        public bool gdprTrackingIncompliant { get; set; }
        public bool showGoodPriceIcon { get; set; }
        public bool nicotineMedicalProduct { get; set; }
        public bool tobaccoFreeNicotineProduct { get; set; }
        public bool nonEkoProduct { get; set; }
        public bool tobaccoProduct { get; set; }
        public List<string> labels { get; set; }
        public string manufacturer { get; set; }
        public double incrementValue { get; set; }
        public ProductBasketType productBasketType { get; set; }
        public bool bargainProduct { get; set; }
        public Thumbnail thumbnail { get; set; }
        public bool outOfStock { get; set; }
        public bool addToCartDisabled { get; set; }
        public bool online { get; set; }
        public string displayVolume { get; set; }
        public string name { get; set; }
        public string code { get; set; }
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
        public object freeTextSearch { get; set; }
        public object categoryCode { get; set; }
        public object keywordRedirectUrl { get; set; }
        public object spellingSuggestion { get; set; }
        public object categoryName { get; set; }
        public object customSuggestion { get; set; }
        public object categoryBreadcrumbs { get; set; }
        public object navigationCategories { get; set; }
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

