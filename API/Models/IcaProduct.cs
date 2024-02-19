// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace API.Models;

public class Attribute
{
    public string icon { get; set; }
    public string label { get; set; }
}

public class Catchweight
{
    public MinQuantity minQuantity { get; set; }
    public TypicalQuantity typicalQuantity { get; set; }
    public MaxQuantity maxQuantity { get; set; }
}

public class Counter
{
    public List<string?> productIds { get; set; }
    public int requiredProductQuantity { get; set; }
}

public class Current
{
    public string amount { get; set; }
    public string currency { get; set; }
}

public class Icons
{
    public List<object> certification { get; set; }
}

public class Image
{
    public string src { get; set; }
    public string description { get; set; }
    public string fopSrcset { get; set; }
    public string bopSrcset { get; set; }
}

public class Image2
{
    public string src { get; set; }
    public string description { get; set; }
    public string fopSrcset { get; set; }
    public string bopSrcset { get; set; }
}

public class MaxQuantity
{
    public string value { get; set; }
    public string uom { get; set; }
}

public class MinQuantity
{
    public string value { get; set; }
    public string uom { get; set; }
}

public class MissedPromotion
{
    public string promoId { get; set; }
    public string retailerPromotionId { get; set; }
    public string countBy { get; set; }
    public List<Counter> counters { get; set; }
}

public class Offer
{
    public string id { get; set; }
    public string retailerPromotionId { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public string presentationMode { get; set; }
    public int? requiredProductQuantity { get; set; }
}

public class Offer2
{
    public string id { get; set; }
    public string description { get; set; }
    public string type { get; set; }
    public string retailerPromotionId { get; set; }
    public string presentationMode { get; set; }
    public int? requiredProductQuantity { get; set; }
}

public class Original
{
    public string? amount { get; set; }
    public string currency { get; set; }
}

public class Price
{
    public Original original { get; set; }
    public Current current { get; set; }
    public Unit unit { get; set; }
}

public class Product
{
    public string productId { get; set; }
    public string retailerProductId { get; set; }
    public string name { get; set; }
    public bool available { get; set; }
    public bool maxQuantityReached { get; set; }
    public List<string> imagePaths { get; set; }
    public List<object> alternatives { get; set; }
    public Price price { get; set; }
    public bool isInCurrentCatalog { get; set; }
    public bool isInProductList { get; set; }
    public string brand { get; set; }
    public List<object> retailerFinancingPlanIds { get; set; }
    public Image image { get; set; }
    public List<Image> images { get; set; }
    public Icons icons { get; set; }
    public List<Offer> offers { get; set; }
    public Offer offer { get; set; }
    public Size size { get; set; }
    public string featured { get; set; }
    public List<Attribute> attributes { get; set; }
    public string countryOfOrigin { get; set; }
    public Catchweight catchweight { get; set; }
}

public class IcaProducts
{
    public List<Product>? products { get; set; }
    public List<MissedPromotion> missedPromotions { get; set; }
}

public class Size
{
    public string value { get; set; }
    public string uom { get; set; }
    public bool? catchWeight { get; set; }
}

public class TypicalQuantity
{
    public string value { get; set; }
    public string uom { get; set; }
}

public class Unit
{
    public string label { get; set; }
    public Original original { get; set; }
    public Current current { get; set; }
}