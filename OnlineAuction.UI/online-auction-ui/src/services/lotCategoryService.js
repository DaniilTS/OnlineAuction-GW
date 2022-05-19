export async function getlotCategories() {
    const response = await fetch(`https://localhost:44365/api/lot/categories`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
    });

    return await response.json();
}

export async function create(name) {
    const body = new FormData();
    body.append('name', name);

    const response = await fetch(`https://localhost:44365/api/lot/category`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
        body: body
    });
}