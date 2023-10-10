using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage) {
            _appStorage = appStorage;
        }
        public HashSet<Recipe> GetRecipesByIngredient(int? id, string? name)
        {
            Ingredient ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            // search using ID and name not provided
            if (id != null && name == null)
            {
                ingredient = _appStorage.Ingredients.First(i => i.Id == id);

                // if ingredient is null, then return an empty hashset which will result in a NotFound error
                if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

                recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
            }

            // search using name and ID not provided
            if (name != null && id == null)
            {
                ingredient = _appStorage.Ingredients.First(i => i.Name.Contains(name));

                // if ingredient is null, then return an empty hashset which will result in a NotFound error
                if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

                recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
            }

            // search using both ID and name
            if (name != null && id  != null)
            {
                ingredient = _appStorage.Ingredients.First(i => i.Id == id);

                // if ingredient searched by id returns null, then search by name
                if (ingredient == null)
                {
                    ingredient = _appStorage.Ingredients.First(i => i.Name.Contains(name));
                
                    // if ingredient is still null, return an empty hashset of recipies which will result in a NotFound error
                } if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

                recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
            }

            return recipes;
        }
    }
}
