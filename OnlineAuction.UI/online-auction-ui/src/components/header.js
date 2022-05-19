import '../styles/header.scss';
import UserPhotoNoImage from '../media/user-no-photo.svg';
import { getUserPhoto } from '../services/photoService.js';
import { useState } from 'react';
import { getUser } from '../services/userService';

function AppHeader({ isAuth, isLoginPage, isSignupPage, setIsAuth, setIsLoginPage, setIsSignupPage }) {

  function createLoginDialog() {
    setIsLoginPage(true);
    setIsSignupPage(false);
  }

  function createSignupDialog() {
    setIsLoginPage(false);
    setIsSignupPage(true);
  }

  function logout() {
    setIsAuth(false);
    setIsLoginPage(true);
    setIsSignupPage(false);
    localStorage.setItem('accessToken', null);
  }

  const [ url, setUrl ] = useState(UserPhotoNoImage);
  const [ user, setUser ] = useState()

  if (isAuth) {
    if(url == UserPhotoNoImage){
      getUserPhoto().then(value => {
        setUrl(value);
        console.log('photo')
      });
    }
    
    if(!user){
      getUser().then(val => {
        setUser(val);
        console.log('set user');
      })
    }
  }
  
  const authBtns = (isAuth && user 
    ? (
        <div className={"header_authenticated"}>
          <p>{user.fullName.firstName}</p>
          <img id='user-photo' className="header_authenticated__image" src={url}/>
          <button className="auth-btns__button" onClick={() => logout()}>Log out</button>
        </div>
    ) 
    : (
      <div className={ isLoginPage || isSignupPage ? "header__auth-btn" : "header__auth-btns"}>
          { isLoginPage ? '' : <button className="auth-btns__button" onClick={() => createLoginDialog()}>Log in</button>}
          { isSignupPage ? '' : <button className="auth-btns__button" onClick={() => createSignupDialog()}>Sign Up</button>}
      </div>
    ))

  return (
      <header className="header">
        <h1 className="header__app-name">OnlineAuction</h1>
        { authBtns }
      </header>
  );
}
  
  export default AppHeader;