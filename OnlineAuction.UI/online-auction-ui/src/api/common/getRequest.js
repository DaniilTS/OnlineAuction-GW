async function getRequest(url, headers, token, responseType) {

    const options = {
        method: "GET",
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            Pragma: 'no-cache',
            ...headers,
        },
    };

    if (token) {
        options.headers.Authorization = `Bearer ${token}`;
    }
    
    let response;
    try {
        response = await fetch(url, options);
        if (response.ok) {
            let data = null;
            if (response.status == 204) {
                return null;
            } else if (responseType == "json") {
                data = await response.json();
            }

            return data;
        }
    } catch (error) {
        console.error(error);
    }
    if (!response.ok) {
       if (response.status === 401) {
            throw "Unauthorized";
        } else if (response.status === 403) {
            throw "Forbidden";
        } else {
            throw "Intenal server error";
        }
    }
}

export async function getJson (url, headers, token) {
    return await getRequest(url, headers, token, "json");
}