using Microsoft.AspNetCore.Mvc;
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
            //arrange
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
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipesExpected = 0;

            //act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }
        #endregion

        #region FilterRecipesByIngredient

        [TestMethod]
        public void FilterRecipesByIngredient_ValidIngredient_ReturnsHashSetOfRecipeIngredients() 
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Ingredient ingredient = new Ingredient
            {
                Id = 1,
                Name = "Spaghetti"
            };
            int recipesExpected = 1;



            // act and assert
            Assert.AreEqual(recipesExpected, bll.FilterRecipesByIngredient(ingredient).Count);
        }

        #endregion

        #region GetRecipesByDiet
        [TestMethod]
        public void GetRecipesByDiet_ValidId_ReturnsRecipesWithIngredients()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietRestrictionId = 4;
            int recipesExpected = 9;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietRestrictionId, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ValidName_ReturnsRecipesWithIngredients()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietRestrictionName = "Nut-Free";
            int recipesExpected = 9;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(null, dietRestrictionName);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ValidIdAndName_ReturnsRecipesWithIngredients()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietRestrictionId = 4;
            string dietRestrictionName = "Nut-Free";
            int recipesExpected = 9;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietRestrictionId, dietRestrictionName);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidId_ReturnsNull()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietRestrictionId = 87;
            int recipesExpected = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietRestrictionId, null);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidName_ReturnsNull()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietRestrictionName = "No-Restriction";
            int recipesExpected = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(null, dietRestrictionName);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidIdAndValidName_ReturnsRecipesWithIngredients()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietRestrictionId = 99;
            string dietRestrictionName = "Nut-Free";
            int recipesExpected = 9;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietRestrictionId, dietRestrictionName);

            //assert
            Assert.AreEqual(recipesExpected, recipes.Count);
        }
        #endregion

        #region FilterRecipesByDiet
        [TestMethod]
        public void FilterRecipesByDiet_ValidDietRestriction_ReturnsRecipesWithoutDietRestriction()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            DietaryRestriction dietaryRestriction = new DietaryRestriction
            {
                Id = 4,
                Name = "Nut-Free"
            };
            int recipesExpected = 9;


            // act and assert
            Assert.AreEqual(recipesExpected, bll.FilterRecipesByDiet(dietaryRestriction).Count);
        }
        #endregion

        #region GetRecipes
        [TestMethod]
        public void GetRecipes_ValidId_ReturnsRecipe()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 2;
            int recipesExpected = 1;

            // act 
            HashSet<Recipe> recipesOutput = bll.GetRecipes(recipeId, null);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }

        [TestMethod]
        public void GetRecipes_ValidName_ReturnsRecipe()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "Chicken Alfredo";
            int recipesExpected = 1;

            // act
            HashSet<Recipe> recipesOutput = bll.GetRecipes(null, recipeName);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }

        [TestMethod]
        public void GetRecipes_ValidIdAndName_ReturnsRecipe()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 2;
            string recipeName = "Chicken Alfredo";
            int recipesExpected = 1;

            // act
            HashSet<Recipe> recipesOutput = bll.GetRecipes(recipeId, recipeName);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }

        [TestMethod]
        public void GetRecipes_InvalidId_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 521;
            int recipesExpected = 0;

            // act
            HashSet<Recipe> recipesOutput = bll.GetRecipes(recipeId, null);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }

        [TestMethod]
        public void GetRecipes_InvalidName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "KFC";
            int recipesExpected = 0;

            // act
            HashSet<Recipe> recipesOutput = bll.GetRecipes(null, recipeName);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }

        [TestMethod]
        public void GetRecipes_InvalidIdAndName_ReturnsNull()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 541;
            string recipeName = "KFC";
            int recipesExpected = 0;

            // act
            HashSet<Recipe> recipesOutput = bll.GetRecipes(recipeId, recipeName);

            // assert
            Assert.AreEqual(recipesExpected, recipesOutput.Count);
        }
        #endregion

        #region DeleteIngredient
        [TestMethod]
        public void DeleteIngredient_ValidId_ReturnsNullString()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 4;
            string? expectedMsg = null;

            // act 
            string? actualMsg = bll.DeleteIngredient(ingredientId, null);

            //assert
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void DeleteIngredient_ValidName_ReturnsNullString()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Tomatoes";
            string? expectedMsg = null;

            // act 
            string? actualMsg = bll.DeleteIngredient(null, ingredientName);

            //assert
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void DeleteIngredient_InvalidIdAndValidName_ReturnsNullString()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 5436;
            string ingredientName = "Tomatoes";
            string? expectedMsg = null;

            // act 
            string? actualMsg = bll.DeleteIngredient(ingredientId, ingredientName);

            //assert
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void DeleteIngredient_InvalidId_ThrowsArgumentNullExcemption()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 5436;

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                bll.DeleteIngredient(ingredientId, null);
            });
        }

        [TestMethod]
        public void DeleteIngredient_InvalidName_ThrowsArgumentNullExcemption()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Roma tomatoes";

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                bll.DeleteIngredient(null, ingredientName);
            });
        }

        [TestMethod]
        public void DeleteIngredient_InvalidIdAndInvalidName_ThrowsArgumentNullExcemption()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 5436;
            string ingredientName = "Roma tomatoes";

            //act and assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                bll.DeleteIngredient(ingredientId, ingredientName);
            });
        }
        #endregion

        #region DeleteIngredientHelperMethod
        [TestMethod]
        public void DeleteIngredientHelperMethod_OneRecipeAssociated_ReturnsNullMessage()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Ingredient ingredient = new Ingredient
            {
                Id = 4,
                Name = "Tomatoes"
            };
            string? ExpectedMsg = null;

            // act
            bll.DeleteIngredientHelperMethod(ingredient);

            //assert
            Assert.IsNull(ExpectedMsg);
        }

        [TestMethod]
        public void DeleteIngredientHelperMethod_ManyRecipesAssociated_ReturnsForbiddenMessage()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Ingredient ingredient = new Ingredient
            {
                Id = 6,
                Name = "Salmon"
            };
            string ExpectedMsg = "Ingredient is associated with more than 1 recipe.";

            // act
            string actualMsg = bll.DeleteIngredientHelperMethod(ingredient);

            //assert
            Assert.AreEqual(ExpectedMsg, actualMsg);
        }
        #endregion

        #region DeleteRecipe
        [TestMethod]
        public void DeleteRecipe_ValidId_DeletesRecipe()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Recipe recipe = new Recipe
            {
                Id = 1,
            };
            string expectedMsg = "Recipe deleted successfully";

            string actualMsg = bll.DeleteRecipe(recipe);

            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void DeleteRecipe_ValidName_DeletesRecipe()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Recipe recipe = new Recipe
            {
                Name = "Spaghetti Carbonara",
            };
            string expectedMsg = "Recipe deleted successfully";

            string actualMsg = bll.DeleteRecipe(recipe);

            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void DeleteRecipe_InValidIdAndInvalidName_ThrowsArgumentNullException()
        {
            BusinessLogicLayer bll = _initializeBusinessLogic();
            Recipe recipe = new Recipe
            {
                Id = 5416,
                Name = "sdvksklvndsvb",
                Description = "Classic Roman pasta dish with eggs, cheese, and pancetta.",
                Servings = 2
            };

            Assert.ThrowsException<ArgumentNullException>(() => bll.DeleteRecipe(recipe));
        }
        #endregion
    }
}