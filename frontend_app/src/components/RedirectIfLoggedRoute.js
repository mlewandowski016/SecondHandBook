import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

export const RedirectIfLoggedRoute = ({ children }) => {
    const { user } = useAuth();
    const location = useLocation();
    
    if (!user) {
        return children;
    }

    return <Navigate to={"/"}/>;
};
