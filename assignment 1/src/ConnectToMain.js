import curUser from './CurrentUser.js';
import users from './DataBase.js';
import userIdx from './UserIdx.js';

function ConnectToMain(username, password, navigate) {
    curUser.value = username;
    navigate('../main', { replace: true });
}

export default ConnectToMain;