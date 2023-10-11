using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;

namespace RecipeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }

        #region GetRecipesByIngredient
        [TestMethod]
        public void GetRecipesByIngredient_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipesExpected = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Cocoa Powder";
            int recipesExpected = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            // assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        
        #endregion
    }
}