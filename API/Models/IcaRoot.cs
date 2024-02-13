
namespace API.Models
{




    public class AdditionalProductAttribute
    {
    }



    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public int productCount { get; set; }
        public string fullURLPath { get; set; }
        public string retailerCategoryId { get; set; }
        public List<object> children { get; set; }
    }
    
    public class DepositPrice
    {
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class Entities
    {
        public Product product { get; set; }
    }

    public class Filter
    {
        public string id { get; set; }
        public string label { get; set; }
        public List<Attribute> attributes { get; set; }
        public string type { get; set; }
    }

    
    public class ProductGroup
    {
        public string type { get; set; }
        public List<string> products { get; set; }
        public string name { get; set; }
        public List<object> clusterBreadcrumbs { get; set; }
        public List<AdditionalProductAttribute> additionalProductAttributes { get; set; }
    }

    public class Result
    {
        public List<ProductGroup> productGroups { get; set; }
        public List<object> breadcrumbs { get; set; }
        public List<SortOption> sortOptions { get; set; }
        public List<Category> categories { get; set; }
        public List<Filter> filters { get; set; }
        public int totalProducts { get; set; }
        public string fullURLPath { get; set; }
        public List<MissedPromotion> missedPromotions { get; set; }
    }

    public class IcaRoot
    {
        public Entities entities { get; set; }
        public Result result { get; set; }
    }


    public class SortOption
    {
        public string id { get; set; }
        public string messageKey { get; set; }
        public bool selected { get; set; }
    }


}


