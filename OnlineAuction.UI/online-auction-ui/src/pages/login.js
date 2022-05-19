import '../styles/login.scss';
import { login } from '../services/authService.js';

function Login(props) {

    return (
        <div className="login">
            <input id='Email' placeholder='Email' className="login__input" type={"text"}/>
            <input id='Password' placeholder='Password' className="login__input" type={"password"}/>
            <button className='login__button' onClick={() => login(props)}>Login</button>
        </div>
    );
}
  
export default Login;