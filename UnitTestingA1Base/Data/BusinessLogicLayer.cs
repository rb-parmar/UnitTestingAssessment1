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
            Ingredient? ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            // search using ID and name not provided
            if (id != null && name == null)
            {
                ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);

                // if ingredient is null, then return an empty hashset which will result in a NotFound error
                if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByIngredient(ingredient);
            }

            // search using name and ID not provided
            if (name != null && id == null)
            {
                ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name.Contains(name));

                // if ingredient is null, then return an empty hashset which will result in a NotFound error
                if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByIngredient(ingredient);
            }

            // search using both ID and name
            if (name != null && id  != null)
            {
                ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);

                // if ingredient searched by id returns null, then search by name
                if (ingredient == null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name.Contains(name));
                } 
                
                // if ingredient is still null, return an empty hashset of recipies which will result in a NotFound error
                if (ingredient == null)
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByIngredient(ingredient);
            }

            return recipes;
        }

        // helper method to filter out recipes if they do not contain the provided ingredient
        public HashSet<Recipe> FilterRecipesByIngredient(Ingredient ingredient)
        {
            HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

            return _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
        }

        public HashSet<Recipe> GetRecipesByDiet(int? id, string? name)
        {
            DietaryRestriction dietaryRestriction;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            // search using ID and name not provided
            if (id != null && name == null)
            {
                dietaryRestriction = _appStorage.DietaryRestrictions.First(i => i.Id == id);

                if (dietaryRestriction == null)
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByDiet(dietaryRestriction);
            }

            // search using ID and name not provided
            if (name != null && id == null)
            {
                dietaryRestriction = _appStorage.DietaryRestrictions.First(i => i.Name.Contains(name));

                if (dietaryRestriction == null)
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByDiet(dietaryRestriction);
            }

            // search using both ID and name
            if (id != null && name != null) 
            {
                dietaryRestriction = _appStorage.DietaryRestrictions.First(d => d.Id == id);
                
                // if dietaryRestriction searched by id returned null, then search by name
                if (dietaryRestriction == null)
                {
                    dietaryRestriction = _appStorage.DietaryRestrictions.First(d => d.Name.Contains(name));
                } 
                
                // if the dietaryRestriction searched by name returned null, then return a new hashset of recipes which will return a NotFound error.
                if (dietaryRestriction == null) 
                {
                    return new HashSet<Recipe>();
                }

                recipes = FilterRecipesByDiet(dietaryRestriction);
            }

            return recipes;
        }

        // helper method to filter out recipes that contain an ingredient in a certain dietary restriction 
        public HashSet<Recipe> FilterRecipesByDiet(DietaryRestriction dietaryRestriction)
        {
            HashSet<Recipe> filteredRecipes = new HashSet<Recipe>();

            // Find ingredients that belong to a certain dietary restriction
            HashSet<IngredientRestriction> ingredientRestrictions = _appStorage.IngredientRestrictions.Where(ir => ir.DietaryRestrictionId == dietaryRestriction.Id).ToHashSet();

            HashSet<RecipeIngredient> recipeIngredients = new HashSet<RecipeIngredient>();

            HashSet<RecipeIngredient> RecipesInDb = _appStorage.RecipeIngredients;

            // iterate over every recipie
            foreach (IngredientRestriction IR in ingredientRestrictions)
            {
                // set an ingredient that matched the ingredientId of the IR
                Ingredient ingredient = _appStorage.Ingredients.First(i => i.Id == IR.IngredientId);

                // Now, iterate over every RecipeIngredient
                foreach (RecipeIngredient RI in RecipesInDb)
                {
                    // if the RecipeIngredientID does not match the set ingredient's id
                    if (RI.IngredientId != ingredient.Id)
                    {
                        // add that recipe to the recipeIngredients
                        recipeIngredients.Add(RI);
                        RecipesInDb.Remove(RI);
                    }
                }
            }

            filteredRecipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();

            return filteredRecipes;
        }

        public HashSet<Recipe> GetRecipes(int? id, string? name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();
            
            // search using id and name not provided
            if (id != null && name == null)
            {
                recipes = _appStorage.Recipes.Where(r => r.Id == id).ToHashSet();
            }

            // search using name and id not provided
            if (name != null && id == null)
            {
                recipes = _appStorage.Recipes.Where(r => r.Name.Contains(name)).ToHashSet();
            }

            // search using both id and name
            if (id != null && name != null)
            {
                // search using id 
                recipes = _appStorage.Recipes.Where(r => r.Id == id).ToHashSet();

                // if the recipe count is less than or equal to 0, search by name
                if (recipes.Count <= 0)
                {
                    recipes = _appStorage.Recipes.Where(r => r.Name.Contains(name)).ToHashSet();
                }
            }

            return recipes;
        }

        public string? DeleteIngredient(int? id, string? name)
        {
            string? message = null;
            Ingredient ingredient;

            // search using id and name not provided
            if (id != null && name == null)
            {
                ingredient = _appStorage.Ingredients.First(r => r.Id == id);

                message = DeleteIngredientHelperMethod(ingredient);
            }

            // search using name and id not provided
            if (id == null && name != null)
            {
                ingredient = _appStorage.Ingredients.First(r => r.Name.Contains(name));

                message = DeleteIngredientHelperMethod(ingredient);
            }

            // search using both id and name
            if (id != null && name != null)
            {
                ingredient = _appStorage.Ingredients.First(r => r.Id == id);

                if (ingredient == null)
                {
                    ingredient = _appStorage.Ingredients.First(r => r.Name.Contains(name));
                }

                if (ingredient == null)
                {
                    return "Ingredient not found.";
                }

                message = DeleteIngredientHelperMethod(ingredient);
            }

            return message;
        }

        // Helper method to delete associated recipes, recipeIngredients and ingredient
        public string? DeleteIngredientHelperMethod(Ingredient ingredient)
        {
            string? msg = null;

            HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(ri => ri.IngredientId == ingredient.Id).ToHashSet();

            if (recipeIngredients.Count == 1)
            {
                Recipe recipe = _appStorage.Recipes.First(r => r.Id == recipeIngredients.First().RecipeId);

                _appStorage.Recipes.Remove(recipe);
                _appStorage.RecipeIngredients.Remove(recipeIngredients.First());
                _appStorage.Ingredients.Remove(ingredient);
            } else
            {
                msg = "Ingredient is associated with more than 1 recipe.";
            }

            return msg;
        }

        public void DeleteRecipe(Recipe newRecipe)
        {
            Recipe recipe = new Recipe();

            if (newRecipe.Id != 0)
            {
                recipe = _appStorage.Recipes.First(r => r.Id == newRecipe.Id);
            } else if (newRecipe.Name != null)
            {
                recipe = _appStorage.Recipes.First(r => r.Name.Contains(newRecipe.Name));
            }

            HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(ri => ri.RecipeId  == recipe.Id).ToHashSet();

            foreach (RecipeIngredient RI in recipeIngredients)
            {
                _appStorage.RecipeIngredients.Remove(RI);
            }

            _appStorage.Recipes.Remove(recipe);
        }
    }
}
