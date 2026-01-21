import { useEffect, useState } from 'react'
import axios from 'axios'

function App() {
  const [obras, setObras] = useState([])
  const carregarObras = async () => {
    try {
      const resposta = await axios.get('http://localhost:5216/api/obras')
      setObras(resposta.data)
      } catch (error) {
      console.error('Erro ao carregar obras:', error)
    }
  }

  useEffect(() => {
    carregarObras()
  }, [])

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial, sans-serif' }}>
      <h1>Galeria Almirante</h1>
      <hr />
      <ul>
        {obras.map((obra) => (
          <li key={obra.id} style={{ marginBottom: '15px' }}>
            <strong>{obra.titulo}</strong> - {obra.autor} 
          </li>
        ))}
      </ul>
      </div>
  )
}

export default App