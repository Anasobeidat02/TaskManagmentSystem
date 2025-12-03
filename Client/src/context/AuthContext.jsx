import { createContext, useState, useEffect } from 'react';
import api from '../api/axios';
import { toast } from 'react-toastify';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            // Optionally validate token or fetch user info here
            // For now, we'll just assume if token exists, user is logged in (or decode it)
             api.get('/api/Users/get-current-user-info')
                .then(res => setUser(res.data))
                .catch(() => {
                    localStorage.removeItem('token');
                    setUser(null);
                })
                .finally(() => setLoading(false));
        } else {
            setLoading(false);
        }
    }, []);

    const login = async (username, password) => {
        try {
            const response = await api.post('/api/Users/login', { UserName: username, Password: password });
            const { token, ...userData } = response.data;
            localStorage.setItem('token', token);
            setUser(userData);
            toast.success("Login successful!");
            return true;
        } catch (error) {
            console.error("Login error", error);
            toast.error(error.response?.data || "Login failed");
            return false;
        }
    };

    const register = async (fullName, email, password) => {
        try {
            await api.post('/api/Users', { FullName: fullName, EmailAddress: email, Password: password });
            toast.success("Registration successful! Please login.");
            return true;
        } catch (error) {
            console.error("Registration error", error);
            toast.error(error.response?.data || "Registration failed");
            return false;
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        setUser(null);
        toast.info("Logged out");
    };

    return (
        <AuthContext.Provider value={{ user, login, register, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};
