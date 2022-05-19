export async function getUser() {
    const response = await fetch('https://localhost:44365/api/User/', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
    });

    return await response.json();
}

export async function getUsers(currentPage, pageSize) {
    const response = await fetch(`https://localhost:44365/api/User/all?PageSize=${pageSize}&Page=${currentPage}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
    });

    return await response.json();
}