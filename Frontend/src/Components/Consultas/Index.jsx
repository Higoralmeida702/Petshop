import React, { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';

const Consultas = () => {
  const [consultas, setConsultas] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [editingConsulta, setEditingConsulta] = useState(null);
  const [form, setForm] = useState({
    exame: "",
    statusConsulta: "",
    statusExame: "",
    agendarDia: "",
    valor: "",
    animalId: "",
  });

  const exameOptions = [
    { value: "1", label: "Rotina" },
    { value: "2", label: "Tosa" },
    { value: "3", label: "Exame de sangue" },
    { value: "4", label: "Exame de pele" },
    { value: "5", label: "Exame de urina" },
    { value: "6", label: "Exame de fezes" },
  ];

  const statusConsultaOptions = [
    { value: "1", label: "Em andamento" },
    { value: "2", label: "Consulta finalizada" },
  ];

  const statusExameOptions = [
    { value: "1", label: "Em coleta" },
    { value: "2", label: "Em espera" },
    { value: "3", label: "Exame realizado" },
  ];

  const fetchConsultas = async () => {
    try {
      const response = await fetch("http://localhost:5155/api/Consulta/ObterTodasConsultas");
      if (!response.ok) throw new Error("Erro: " + response.statusText);
      const data = await response.json();
      setConsultas(data);
    } catch (error) {
      console.error("Erro ao buscar consultas:", error);
    }
  };

  useEffect(() => {
    fetchConsultas();
  }, []);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const payload = {
      exame: parseInt(form.exame),
      statusConsulta: parseInt(form.statusConsulta),
      statusExame: parseInt(form.statusExame),
      agendarDia: form.agendarDia,
      valor: parseFloat(form.valor),
      animalId: parseInt(form.animalId),
    };

    try {
      if (editingConsulta) {
        const response = await fetch(`http://localhost:5155/api/Consulta/EditarConsulta/${editingConsulta.id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(payload),
        });
        if (!response.ok) throw new Error("Erro ao atualizar: " + response.statusText);
      } else {
        const response = await fetch("http://localhost:5155/api/Consulta/AdicionarConsulta", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(payload),
        });
        if (!response.ok) throw new Error("Erro ao adicionar: " + response.statusText);
      }
      await fetchConsultas();
      setEditingConsulta(null);
      setForm({ exame: "", statusConsulta: "", statusExame: "", agendarDia: "", valor: "", animalId: "" });
    } catch (error) {
      console.error("Erro no envio:", error);
    }
  };

  const handleEdit = (consulta) => {
    setEditingConsulta(consulta);
    setForm({
      exame: String(consulta.exame),
      statusConsulta: String(consulta.statusConsulta),
      statusExame: String(consulta.statusExame),
      agendarDia: consulta.agendarDia.includes("T") ? consulta.agendarDia.split("T")[0] : consulta.agendarDia,
      valor: consulta.valor,
      animalId: String(consulta.animalId),
    });
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`http://localhost:5155/api/Consulta/ExcluirConsulta/${id}`, { method: "DELETE" });
      if (!response.ok) throw new Error("Erro ao deletar: " + response.statusText);
      await fetchConsultas();
    } catch (error) {
      console.error("Erro ao deletar consulta:", error);
    }
  };

  const filteredConsultas = consultas.filter((consulta) => {
    const searchTermLower = searchTerm.toLowerCase();
    return (
      consulta.exame.toLowerCase().includes(searchTermLower) ||
      consulta.statusConsulta.toLowerCase().includes(searchTermLower) ||
      consulta.statusExame.toLowerCase().includes(searchTermLower) ||
      consulta.agendarDia.toLowerCase().includes(searchTermLower)
    );
  });

  return (
    <div className="container mt-4">
      <h1 className="mb-4">Lista de Consultas</h1>
      <div className="mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Buscar por exame, status ou dia"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>
      {filteredConsultas.length > 0 ? (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>ID</th>
              <th>Exame</th>
              <th>Status Consulta</th>
              <th>Status Exame</th>
              <th>Agendar Dia</th>
              <th>Valor</th>
              <th>Animal ID</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {filteredConsultas.map((consulta) => (
              <tr key={consulta.id}>
                <td>{consulta.id}</td>
                <td>{consulta.exame}</td>
                <td>{consulta.statusConsulta}</td>
                <td>{consulta.statusExame}</td>
                <td>{new Date(consulta.agendarDia).toLocaleDateString()}</td>
                <td>{consulta.valor}</td>
                <td>{consulta.animalId}</td>
                <td>
                  <button className="btn btn-sm btn-primary me-2" onClick={() => handleEdit(consulta)}>Editar</button>
                  <button className="btn btn-sm btn-danger" onClick={() => handleDelete(consulta.id)}>Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Nenhuma consulta encontrada.</p>
      )}

      <div className="card mt-4">
        <div className="card-header">{editingConsulta ? "Editar Consulta" : "Adicionar Consulta"}</div>
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="exame" className="form-label">Exame</label>
              <select name="exame" id="exame" className="form-select" value={form.exame} onChange={handleChange} required>
                <option value="">Selecione o exame</option>
                {exameOptions.map(opt => (
                  <option key={opt.value} value={opt.value}>{opt.label}</option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="statusConsulta" className="form-label">Status Consulta</label>
              <select name="statusConsulta" id="statusConsulta" className="form-select" value={form.statusConsulta} onChange={handleChange} required>
                <option value="">Selecione o status</option>
                {statusConsultaOptions.map(opt => (
                  <option key={opt.value} value={opt.value}>{opt.label}</option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="statusExame" className="form-label">Status Exame</label>
              <select name="statusExame" id="statusExame" className="form-select" value={form.statusExame} onChange={handleChange} required>
                <option value="">Selecione o status</option>
                {statusExameOptions.map(opt => (
                  <option key={opt.value} value={opt.value}>{opt.label}</option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label htmlFor="agendarDia" className="form-label">Agendar Dia</label>
              <input type="date" name="agendarDia" id="agendarDia" className="form-control" value={form.agendarDia} onChange={handleChange} required />
            </div>

            <div className="mb-3">
              <label htmlFor="valor" className="form-label">Valor</label>
              <input type="number" name="valor" id="valor" className="form-control" value={form.valor} onChange={handleChange} step="0.01" min="0" required />
            </div>

            <div className="mb-3">
              <label htmlFor="animalId" className="form-label">Animal ID</label>
              <input type="number" name="animalId" id="animalId" className="form-control" value={form.animalId} onChange={handleChange} required />
            </div>

            <button type="submit" className="btn btn-success">{editingConsulta ? "Atualizar" : "Adicionar"}</button>
            {editingConsulta && (
              <button
                type="button"
                className="btn btn-secondary ms-2"
                onClick={() => {
                  setEditingConsulta(null);
                  setForm({ exame: "", statusConsulta: "", statusExame: "", agendarDia: "", valor: "", animalId: "" });
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

export default Consultas;
