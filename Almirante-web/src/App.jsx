import { useEffect, useState } from 'react'
import axios from 'axios'

function App() {
  const [obras, setObras] = useState([])
  const [mostrarModal, setMostrarModal] = useState(false)
  const [novaObra, setNovaObra] = useState({ titulo: '', autor: '', urlImagem: '' });

  // 1. Buscar obras do C#
  const carregarObras = async () => {
    try {
      const resposta = await axios.get ('http://localhost:5216/api/Obras')
      setObras(resposta.data)
    } catch (error) {
      console.error("Erro ao carregar:", error)
    }
  }

  // 2. Enviar nova obra para o C#
  const salvarObra = async (e) => {
    e.preventDefault()
    try {
      await axios.post('http://localhost:5216/api/Obras', novaObra)
      setMostrarModal(false) // Fecha a janela
      setNovaObra({ titulo: '', autor: '', urlImagem: '' }) // Limpa os campos
      carregarObras() // Atualiza a lista na tela
    } catch (error) {
      alert("Erro ao salvar!")
    }
  }

// 3. Exclui a obra
const excluirObra = async (id) => {
  if (window.confirm("Tem certeza que quer excluir essa obra?")) {
  try {
    await axios.delete(`http://localhost:5216/api/Obras/${id}`);
    alert("Obra excluída com sucesso!");
    carregarObras(); // Atualiza a lista na tela
  } catch (error) {
    console.error(error);
    alert("Erro ao excluir!")
  }
}
};

useEffect(() => { 
  carregarObras();
}, []);

  return (
    <div style={{ backgroundColor: '#121212', color: 'white', minHeight: '100vh', padding: '20px', fontFamily: 'sans-serif' }}>
      <header style={{ textAlign: 'center', marginBottom: '40px' }}>
        <h1 style={{ fontSize: '2.5rem' }}>Almirante<span style={{ color: '#E60023' }}>Gallery</span></h1>
      </header>

      {}
      <div style={{ columns: '4 200px', columnGap: '15px', maxWidth: '1200px', margin: '0 auto' }}>
        {obras.map(obra => (
          <div key={obra.id} style={{ breakInside: 'avoid', marginBottom: '15px', borderRadius: '16px', backgroundColor: '#262626', overflow: 'hidden' }}>
            {/* Botão de excluir */}
            <button
             onClick={() => excluirObra(obra.id)}
             style={{ position: 'absolute', top: '10px', right: '10px', backgroundColor: '#E60023', color: 'white', border: 'none', borderRadius: '50%', cursor: 'pointer', width: '25px', height: '25px' }}
             >
              X
            </button>

            <img src={obra.urlImagem} alt={obra.titulo} style={{ width: '100%', display: 'block' }} />
            <div style={{ padding: '10px' }}>
              <h4 style={{ margin: '0' }}>{obra.titulo}</h4>
              <p style={{ margin: '5px 0 0', fontSize: '0.8rem', color: '#aaa' }}>{obra.autor}</p>
            </div>
          </div>
        ))}
      </div>

      {/* Botão Flutuante */}
      <button 
        onClick={() => setMostrarModal(true)}
        style={{ position: 'fixed', bottom: '30px', right: '30px', width: '60px', height: '60px', borderRadius: '50%', backgroundColor: '#E60023', color: 'white', border: 'none', fontSize: '30px', cursor: 'pointer' }}
      >+</button>

      {/* Modal de Cadastro (Só aparece se mostrarModal for true) */}
      {mostrarModal && (
        <div style={{ position: 'fixed', top: 0, left: 0, width: '100%', height: '100%', backgroundColor: 'rgba(0,0,0,0.8)', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
          <form onSubmit={salvarObra} style={{ backgroundColor: '#262626', padding: '30px', borderRadius: '20px', width: '300px', display: 'flex', flexDirection: 'column', gap: '10px' }}>
            <h2>Nova Obra</h2>
            <input placeholder="Título" required value={novaObra.titulo} onChange={e => setNovaObra({...novaObra, titulo: e.target.value})} style={{ padding: '10px', borderRadius: '5px' }} />
            <input placeholder="Autor" required value={novaObra.autor} onChange={e => setNovaObra({...novaObra, autor: e.target.value})} style={{ padding: '10px', borderRadius: '5px' }} />
            <input placeholder="URL da Imagem" required value={novaObra.urlImagem} onChange={e => setNovaObra({...novaObra, urlImagem: e.target.value})} style={{ padding: '10px', borderRadius: '5px' }} />
            <button type="submit" style={{ backgroundColor: '#E60023', color: 'white', padding: '10px', border: 'none', borderRadius: '5px', cursor: 'pointer' }}>Salvar no Banco</button>
            <button type="button" onClick={() => setMostrarModal(false)} style={{ background: 'none', color: 'white', border: 'none', cursor: 'pointer' }}>Cancelar</button>
          </form>
        </div>
      )}
    </div>
  )
}

export default App