async function bodyRequest (method, url, headers, body, token, responseType) {
    const options = {
        method,
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Pragma: 'no-cache',
            ...headers,
        }
    };
    if (body) {
        options.body = JSON.stringify(body);
    }
    if (token) {
        options.headers.Authorization = `Bearer ${token}`;
    }
    let response;
    try {
        response = await fetch(url, options);
        if (response.ok) {
            let data = null;
            if (responseType == "json") {
                data = await response.json();
            }
            return data;
        }
    } catch (error) {
        console.log(error);
    }
    if (!response.ok) {
        if (response.status === 401) {
            throw "Unauthorized";
        } else if (token && response.status === 403) {
            throw "Forbidden";
        } else if (!token && (response.status === 404 || response.status === 400)) {
            throw "Bad login data";
        } else {
            throw "Server error";
        }
    }
}

export async function post (url, headers, body, token) {
    return await bodyRequest("POST", url, headers, body, token, "");
}

export async function postJson (url, headers, body, token) {
    return await bodyRequest("POST", url, headers, body, token, "json");
}