import React, { useState } from 'react';
import AppFooter from './components/footer';
import AppHeader from './components/header';
import AppMain from './components/main.js';
import './styles/App.css';

function App() {

  const [ isAuth, setIsAuth ] = useState(localStorage.getItem("accessToken"));
  const [ isLoginPage, setIsLoginPage ] = useState(false);
  const [ isSignupPage, setIsSignupPage ] = useState(false);

  const params = {
    isAuth,
    isLoginPage,
    isSignupPage,
    setIsAuth,
    setIsLoginPage,
    setIsSignupPage
  }

  return (
    <>
      <AppHeader {...params}/>
      <AppMain {...params}/>
      <AppFooter/>
    </>
  );
}

export default App;
