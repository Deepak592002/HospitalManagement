import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import PatientTable from './components/PatientTable'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <PatientTable/>
    </>
  )
}

export default App
