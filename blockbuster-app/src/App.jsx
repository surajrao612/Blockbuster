import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from './pages/Home'

function App() {
  const [count, setCount] = useState(0)

  return (
    <BrowserRouter>
      <div>
        <h1>BlockBuster App</h1>
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>

      </div>
    </BrowserRouter>
  )
}

export default App
