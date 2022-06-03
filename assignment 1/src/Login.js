import { useRef } from 'react';
import logo from './Pictures/logo.png'
import './Login.css'
import { Link } from 'react-router-dom';
import CheckLogIn from './CheckLogIn.js'
import { useNavigate } from 'react-router-dom';

function Login() {
    const username = useRef(null);
    const labelUsername = useRef(null);
    const password = useRef(null);
    const labelPassword = useRef(null);
    const validationText = useRef(null);
    const navigate = useNavigate();

    function validation(e) {
        e.preventDefault();
        CheckLogIn(username, labelUsername, password, labelPassword, validationText, navigate);
    }

    return (
        <div className="container" id="loginform">
            <div className="center" id="loginblock">
                <form>
                    <img className="mb-4" src={logo} alt="logo" width="72" height="72"></img>
                    <h1 className="h3 mb-3 fw-normal">Welcome<br />Sign in</h1>
                    <div ref={labelUsername} className="form-floating">
                        <input ref={username} type="text" className="form-control" id="floatingUsername" placeholder="Username"></input>
                        <label htmlFor="floatingUsername">Username</label>
                    </div>
                    <div ref={labelPassword} className="form-floating">
                        <input ref={password} type="password" className="form-control" id="floatingPassword" placeholder="Password"></input>
                        <label htmlFor="floatingPassword">Password</label>
                    </div>
                    <div ref={validationText} id="validationText" hidden>Username or password are not correct!</div>
                    <div id="buttons" className="container">
                        <div className="row">
                            <div className="col">
                                <button className="w-100 btn btn-lg btn-primary btn-grad" onClick={validation}>Sign in</button>
                            </div>
                            <div className="col">
                                <Link to="/register"><button className="w-100 btn btn-lg btn-primary btn-grad">Register</button></Link>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Login;