export async function login({ setIsAuth, setIsLoginPage, setIsSignupPage }) {
    const email = document.getElementById('Email');
    const password = document.getElementById('Password');
    
    const body = new FormData();
    body.append('Email', email.value);
    body.append('Password', password.value);

    const response = await fetch('https://localhost:44365/api/Auth/login', {
        method: 'POST',
        headers: {
            'Accept': 'application/json'
        },
        body: body
    });

    const data = await response.json();
    if (response.ok === true) {
        localStorage.setItem('accessToken', data.accessToken.value);
        localStorage.setItem('refreshToken', data.refreshToken.value);
        
        setIsAuth(true);
        setIsLoginPage(false);
        setIsSignupPage(false);
    } else {
        document.getElementById('error-message').innerText = data;
    }
}