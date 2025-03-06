import Home from "./Components/Home/Index"
import Animais from "./Components/Animais/Index"
import Clientes from "./Components/Clientes/Index"
import Estoque from "./Components/Estoque/Index"
import Consultas from "./Components/Consultas/Index"
import Navbar from "./Components/Navbar/Index"
import { BrowserRouter, Link, Routes, Route } from "react-router-dom"
import './styles.css'

function App() {

  return (
    <>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/animais" element={<Animais />} />
          <Route path="/clientes" element={<Clientes />} />
          <Route path="/estoque" element={<Estoque />} />
          <Route path="/consultas" element={<Consultas />} />
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
