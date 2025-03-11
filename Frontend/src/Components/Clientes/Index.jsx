import { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';

const Clientes = () => {
    const [clientes, setClientes] = useState([]);
    const [editingCliente, setEditingCliente] = useState(null);

    const [nome, setNome] = useState("");
    const [numeroTelefone, setNumeroTelefone] = useState("");
    const [endereco, setEndereco] = useState("");

    useEffect(() => {
        fetchClientes();
    }, []);

    const fetchClientes = async () => {
        try {
            const response = await fetch("http://localhost:5155/api/Cliente/ObterTodosClientes");
            if (!response.ok) {
                throw new Error("Erro na requisição: " + response.statusText);
            }
            const data = await response.json();
            setClientes(data);
        } catch (error) {
            console.error("Erro ao buscar clientes:", error);
        }
    };

    const addCliente = async () => {
        try {
            const novoCliente = {
                nome,
                numeroTelefone,
                endereco
            };
            const response = await fetch("http://localhost:5155/api/Cliente/Registrar/Cliente", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(novoCliente)
            });
            if (!response.ok) {
                throw new Error("Erro ao adicionar cliente: " + response.statusText);
            }
            const clienteAdicionado = await response.json();
            setClientes([...clientes, clienteAdicionado]);
            clearForm();
        } catch (error) {
            console.error("Erro ao adicionar cliente:", error);
        }
    };

    const updateCliente = async () => {
        try {
            const clienteAtualizado = {
                nome,
                numeroTelefone,
                endereco
            };
            const response = await fetch(`http://localhost:5155/api/Cliente/${editingCliente.id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(clienteAtualizado)
            });
            if (!response.ok) {
                throw new Error("Erro ao atualizar cliente: " + response.statusText);
            }
            const updatedList = clientes.map(cliente =>
                cliente.id === editingCliente.id ? { ...cliente, ...clienteAtualizado } : cliente
            );
            setClientes(updatedList);
            clearForm();
            setEditingCliente(null);
        } catch (error) {
            console.error("Erro ao atualizar cliente:", error);
        }
    };

    const deleteCliente = async (id) => {
        try {
            const response = await fetch(`http://localhost:5155/api/Cliente/DeletarCliente/${id}`, {
                method: "DELETE"
            });
            if (!response.ok) {
                throw new Error("Erro ao deletar cliente: " + response.statusText);
            }
            setClientes(clientes.filter(cliente => cliente.id !== id));
        } catch (error) {
            console.error("Erro ao deletar cliente:", error);
        }
    };

    const handleEdit = (cliente) => {
        setEditingCliente(cliente);
        setNome(cliente.nome);
        setNumeroTelefone(cliente.numeroTelefone);
        setEndereco(cliente.endereco);
    };

    const clearForm = () => {
        setNome("");
        setNumeroTelefone("");
        setEndereco("");
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (editingCliente) {
            await updateCliente();
        } else {
            await addCliente();
        }
    };

    return (
        <div className="container mt-4">
            <h1 className="mb-4">Lista de Clientes</h1>
            {clientes.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nome</th>
                            <th>Número de Telefone</th>
                            <th>Endereço</th>
                            <th>Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clientes.map((cliente) => (
                            <tr key={cliente.id}>
                                <td>{cliente.id}</td>
                                <td>{cliente.nome}</td>
                                <td>{cliente.numeroTelefone}</td>
                                <td>{cliente.endereco}</td>
                                <td>
                                    <button
                                        className="btn btn-sm btn-primary me-2"
                                        onClick={() => handleEdit(cliente)}
                                    >
                                        Editar
                                    </button>
                                    <button
                                        className="btn btn-sm btn-danger"
                                        onClick={() => deleteCliente(cliente.id)}
                                    >
                                        Excluir
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <div className="alert alert-warning" role="alert">
                    Nenhum cliente encontrado.
                </div>
            )}

            <div className="card mt-4">
                <div className="card-header">
                    {editingCliente ? "Editar Cliente" : "Adicionar Cliente"}
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label className="form-label">Nome</label>
                            <input
                                type="text"
                                className="form-control"
                                value={nome}
                                onChange={(e) => setNome(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Número de Telefone</label>
                            <input
                                type="text"
                                className="form-control"
                                value={numeroTelefone}
                                onChange={(e) => setNumeroTelefone(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Endereço</label>
                            <input
                                type="text"
                                className="form-control"
                                value={endereco}
                                onChange={(e) => setEndereco(e.target.value)}
                                required
                            />
                        </div>
                        <button type="submit" className="btn btn-success">
                            {editingCliente ? "Atualizar" : "Adicionar"}
                        </button>
                        {editingCliente && (
                            <button
                                type="button"
                                className="btn btn-secondary ms-2"
                                onClick={() => {
                                    clearForm();
                                    setEditingCliente(null);
                                }}
                            >
                                Cancelar
                            </button>
                        )}
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Clientes;
