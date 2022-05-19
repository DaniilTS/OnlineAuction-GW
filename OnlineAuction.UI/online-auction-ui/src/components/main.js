import React from 'react';
import Signup from '../pages/signup.js';
import Login from '../pages/login.js';
import '../styles/main.css';
import MainRoutingPage from '../pages/mainRoutingPage.js';

function AppMain(props) {
  return (
    <main className='main'>
      { props.isSignupPage ? <Signup/> : ''}
      { props.isLoginPage ? <Login {...props}/> : ''}
      { props.isAuth ? <MainRoutingPage {...props}/> : ''}
    </main>  
  );
}

export default AppMain;