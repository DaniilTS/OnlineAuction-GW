import React, { useState } from 'react';
import AppFooter from './components/footer';
import AppHeader from './components/header';
// import logo from './media/logo.svg';
import './styles/App.css';

function App() {
  const [ isAuth, setIsAuth ] = useState(false);
  return (
    <>
      <AppHeader isAuth={isAuth} setIsAuth={setIsAuth}/>
      <AppFooter/>
    </>
  );
}

export default App;
