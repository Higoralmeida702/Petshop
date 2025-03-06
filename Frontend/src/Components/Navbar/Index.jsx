import { Link } from 'react-router-dom';
import './Navbar.css'; 

const Navbar = () => {
    return (
        <nav className="navbar">
            <ul className="nav-list">
                <li><Link to="/" className="nav-link">Home</Link></li>
                <li><Link to="/animais" className="nav-link">Gerenciar Animais</Link></li>
                <li><Link to="/clientes" className="nav-link">Gerenciar Clientes</Link></li>
                <li><Link to="/estoque" className="nav-link">Gerenciar Estoque</Link></li>
                <li><Link to="/consultas" className="nav-link">Agendar consultas</Link></li>
            </ul>
        </nav>
    );
}

export default Navbar;
