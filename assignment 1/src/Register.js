import { useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import logo from './Pictures/logo.png'
import InputImage from './InputImage.js';
import CheckRegister from './CheckRegister';
import './Register.css';

function Register() {
    const [result, setResult] = useState("");
    const username = useRef(null);
    const labelUsername = useRef(null);
    const password = useRef(null);
    const labelPassword = useRef(null);
    const verifyPassword = useRef(null);
    const labelVerifyPassword = useRef(null);
    const nickname = useRef(null);
    const labelNickname = useRef(null);
    const imageSrc = useRef(null);
    const validationText = useRef(null);
    const navigate = useNavigate();

    function validation(e) {
        e.preventDefault();
        CheckRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, imageSrc, validationText, navigate);
    }

    return (
        <div className="container" id="registerform">
            <div className="center" id="registerblock">
                <form>
                    <img className="mb-4" src={logo} alt="logo" width="72" height="72"></img>
                    <h1 className="h3 mb-3 fw-normal">Register</h1>
                    <div ref={labelUsername} className="form-floating">
                        <input ref={username} type="text" className="form-control" id="floatingUsername2" placeholder="Username"></input>
                        <label htmlFor="floatingUsername2">Username</label>
                    </div>
                    <div ref={labelPassword} className="form-floating">
                        <input ref={password} type="password" className="form-control" id="floatingPassword2" placeholder="Password"></input>
                        <label htmlFor="floatingPassword2">Password</label>
                    </div>
                    <div ref={labelVerifyPassword} className="form-floating">
                        <input ref={verifyPassword} type="password" className="form-control" id="floatingVerifyPassword" placeholder="Verify Password"></input>
                        <label htmlFor="floatingVerifyPassword">Verify Password</label>
                    </div>
                    <div ref={labelNickname} className="form-floating">
                        <input ref={nickname} type="text" className="form-control" id="floatingNickname" placeholder="Nickname"></input>
                        <label htmlFor="floatingNickname">Nickname</label>
                    </div>
                    {/*<div ref={validationText} id="validationText2" hidden/>
                    <InputImage result={result} setResult={setResult} srcRef={imageSrc} />*/}
                    <div className="container" id="buttons2">
                        <div className="row">
                            <div className="col">
                                <button id="registerBtn" className="w-100 btn btn-lg btn-primary btn-grad" type="submit" onClick={validation}>Register</button>
                            </div>
                            <div className="col">
                                <Link to="/"><button id="backSignInBtn" className="w-100 btn btn-lg btn-primary btn-grad">Back to sign in</button></Link>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Register;