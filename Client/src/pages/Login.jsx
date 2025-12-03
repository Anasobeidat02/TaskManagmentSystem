import { useState, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate, Link } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const success = await login(username, password);
        if (success) {
            navigate('/tasks');
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-r from-gray-300 to-purple-200 flex items-center justify-center px-4">
            <div className="container mx-auto max-w-4xl">
                <div className="bg-white rounded-3xl shadow-2xl flex flex-col lg:flex-row overflow-hidden">
                    {/* Form Section */}
                    <div className="w-full lg:w-1/2 p-8 lg:p-16 flex flex-col justify-center">
                        <h1 className="text-3xl lg:text-4xl font-bold text-gray-800 mb-8">Sign In</h1>

                        {/* <div className="flex justify-center gap-3 mb-6">
                            <a href="#" className="w-12 h-12 rounded-full border border-gray-300 flex items-center justify-center hover:bg-gray-50 transition"><i className="fab fa-google-plus-g text-red-500 text-xl"></i></a>
                            <a href="#" className="w-12 h-12 rounded-full border border-gray-300 flex items-center justify-center hover:bg-gray-50 transition"><i className="fab fa-facebook-f text-blue-600 text-xl"></i></a>
                            <a href="#" className="w-12 h-12 rounded-full border border-gray-300 flex items-center justify-center hover:bg-gray-50 transition"><i className="fab fa-github text-xl"></i></a>
                            <a href="#" className="w-12 h-12 rounded-full border border-gray-300 flex items-center justify-center hover:bg-gray-50 transition"><i className="fab fa-linkedin-in text-blue-700 text-xl"></i></a>
                        </div> */}

                        <p className="text-center text-sm text-gray-600 mb-6">or use your username & password</p>

                        <form className="space-y-4" onSubmit={handleSubmit}>
                            <input 
                                type="text" 
                                placeholder="Username" 
                                className="w-full px-4 py-3 bg-gray-100 rounded-lg focus:outline-none" 
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                required 
                            />
                            <input 
                                type="password" 
                                placeholder="Password" 
                                className="w-full px-4 py-3 bg-gray-100 rounded-lg focus:outline-none" 
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required 
                            />
                            <a href="#" className="block text-right text-sm text-gray-600 hover:underline">Forget Your Password?</a>

                            <button type="submit" className="w-full bg-purple-800 hover:bg-purple-900 text-white font-semibold py-3 rounded-lg uppercase tracking-wider transition mt-4">
                                Sign In
                            </button>
                        </form>
                    </div>

                    {/* Purple Panel */}
                    <div className="hidden lg:flex w-1/2 bg-gradient-to-r from-indigo-500 to-purple-800 text-white p-12 flex-col justify-center items-start">
                        <h1 className="text-4xl font-bold mb-4">Welcome Back!</h1>
                        <p className="text-lg mb-8">Enter your personal details to use all of site features</p>
                        <Link to="/register" className="border-2 border-white px-12 py-3 rounded-lg font-bold uppercase tracking-wider hover:bg-white hover:text-purple-800 transition">
                            Sign Up
                        </Link>
                    </div>
                </div>

                {/* Mobile Link */}
                <p className="text-center mt-8 text-gray-600 lg:hidden">
                    Don't have an account? <Link to="/register" className="text-purple-700 font-bold hover:underline">Sign Up</Link>
                </p>
            </div>
        </div>
    );
};

export default Login;
