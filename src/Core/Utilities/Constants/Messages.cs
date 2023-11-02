namespace Core.Utilities.Constants;

public class Messages
{
    #region Product Messages 
    public const string ProductNotFound = "Product not found"; 
    public const string EmptyProductList = "Product list is empty with the current filter"; 

    public const string CreateProductSuccess = "Product created successfully"; 
    public const string CreateProductError = "Error occured while registering the Product to Database";

    public const string UpdateProductSuccess = "Product successfully updated"; 
    public const string UpdateProductError = "Error occured while updating the Product in Database";  

    public const string DeleteProductSuccess = "Product successfully deleted";
    public const string DeleteProductError = "Error occured while deleting the Product from Database";
    #endregion

    #region Category Messages
    public const string CategoryNotFound = "Category not found";
    public const string EmptyCategoryList = "Category list is empty with the current filter";

    public const string CreateCategorySuccess = "Category created successfully";
    public const string CreateCategoryError = "Error occured while registering the Category to Database";
    public const string CategoryAlreadyExists = "A category already exists with this name";

    public const string UpdateCategorySuccess = "Category successfully updated";
    public const string UpdateCategoryError = "Error occured while updating the Category in Database";

    public const string DeleteCategorySuccess = "Category successfully deleted";
    public const string DeleteCategoryError = "Error occured while deleting the Category from Database";

    public const string EmptyProductListForCategoryError = "This category does not have any products";
    #endregion
}
