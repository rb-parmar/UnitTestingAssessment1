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

        [TestMethod]
        public void GetRecipesByIngredient_ValidIdAndName_ReturnsRecipesWithIngredients()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 9;
            string ingredientName = "Cocoa Powder";
            int recipesExpected = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidId_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 19;
            int recipesExpected = 0;

            //act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByIngredients_InvalidName_ReturnsNull()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Boba";
            int recipesExpected = 0;

            //act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredients_InvalidIdAndName_ReturnsNull()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipesExpected = 0;

            //act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }


        #endregion
    }
}