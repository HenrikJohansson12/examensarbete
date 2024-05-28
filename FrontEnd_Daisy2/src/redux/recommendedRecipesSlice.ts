import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit';
import { RootState } from '../redux/store';
import { RecommendedRecipeDto } from '../data/RecommendedRecipe';
import { fetchRecommendedRecipes } from '../data/fetchRecommendedRecipes';

interface RecipesState {
  recipes: RecommendedRecipeDto[];
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
}

const initialState: RecipesState = {
  recipes: [],
  status: 'idle',
  error: null,
};

// Asynchronous thunk action to fetch recipes
export const fetchRecipes = createAsyncThunk(
  'recipes/fetchRecipes',
  async (token: string, { rejectWithValue }) => {
    try {
      const data = await fetchRecommendedRecipes(token);
      return data;
    } catch (error: any) {
      return rejectWithValue(error.message);
    }
  }
);

const recipesSlice = createSlice({
  name: 'recipes',
  initialState,
  reducers: {
    addRecipe: (state, action: PayloadAction<RecommendedRecipeDto>) => {
      state.recipes.push(action.payload);
    },
    removeRecipe: (state, action: PayloadAction<number>) => {
      state.recipes = state.recipes.filter(recipe => recipe.id !== action.payload);
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchRecipes.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchRecipes.fulfilled, (state, action: PayloadAction<RecommendedRecipeDto[]>) => {
        state.status = 'succeeded';
        state.recipes = action.payload;
      })
      .addCase(fetchRecipes.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message || null;
      });
  },
});

export const { addRecipe, removeRecipe } = recipesSlice.actions;

export const selectAllRecipes = (state: RootState) => state.recipes.recipes;
export const getRecipesStatus = (state: RootState) => state.recipes.status;
export const getRecipesError = (state: RootState) => state.recipes.error;

export default recipesSlice.reducer;
