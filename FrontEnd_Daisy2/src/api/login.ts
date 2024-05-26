interface GoogleLoginResponse {
  token: string;
}

interface AuthResponse {
  tokenType: string;
  accessToken: string;
  expiresIn: number;
  refreshToken: string;
}

export const fetchBearerTokenWithGoogle = async (credential: string): Promise<string | undefined> => {
  try {
    const response = await fetch('https://localhost:7027/api/google-login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ credential }),
    });

    if (!response.ok) {
      throw new Error('Failed to login with Google');
    }

    const data: GoogleLoginResponse = await response.json();
    return data.token;
  } catch (error) {
    console.error('Error during Google login:', error);
  }
};

export const loginWithEmailAndPassword = async (email: string, password: string): Promise<AuthResponse | undefined> => {
  try {
    const response = await fetch('https://localhost:7027/api/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      return undefined
    }

    const data: AuthResponse = await response.json();
    return data;
  } catch (error) {
    console.error('Error during login:', error);
  }
};


export const registerWithEmailAndPassword = async (email: string, password: string): Promise<boolean> => {
  try {
    const response = await fetch('https://localhost:7027/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password }),
    });

    if (response.status==200) {
      return true
    }
    else return false;
    
  } catch (error) {
    console.error('Error during registration:', error);
    return false;
  }
};

  

