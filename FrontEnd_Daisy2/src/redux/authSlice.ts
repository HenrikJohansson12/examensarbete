import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AuthState {
 
  aspNetToken: string | null;
}

const initialState: AuthState = {
 
  aspNetToken: null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {

    setAspNetToken: (state, action: PayloadAction<string>) => {
      state.aspNetToken = action.payload;
    },
    clearTokens: (state) => {
    
      state.aspNetToken = null;
    },
  },
});

export const { setAspNetToken, clearTokens } = authSlice.actions;
export default authSlice.reducer;
