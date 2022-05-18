import '../styles/header.css';

function AppHeader(props) {

  function createLoginDialog() {
    console.log(123);
  }

    return (
        <header className="header">
          <h1 className="header__app-name">OnlineAuction | isAuth</h1>
          <div className="header__auth-btns">
            <button className="auth-btns__button" onClick={() => createLoginDialog()}>Log in</button>
            <button className="auth-btns__button">Sign Up</button>
          </div>
        </header>
    );
  }
  
  export default AppHeader;