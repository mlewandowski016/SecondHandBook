import {Link} from "react-router-dom"

const Navbar = () => {
    return(
    <div>
        <Link to="/">Wystawione książki</Link>
        <Link to="/">Moje książki</Link>
        <Link to="/">Moje konto</Link>
    </div>)
}

export default Navbar;