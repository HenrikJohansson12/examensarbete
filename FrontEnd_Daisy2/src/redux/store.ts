import { configureStore } from '@reduxjs/toolkit';
import authReducer from './authSlice';
import recipesReducer from './recommendedRecipesSlice'
const store = configureStore({
  reducer: {
    auth: authReducer,
    recipes: recipesReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
