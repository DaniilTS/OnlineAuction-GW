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

export async function blockUser(userId, blockState) {
    const body = new FormData();
    body.append('state', blockState);

    const response = await fetch(`https://localhost:44365/api/User/${userId}/block`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
        body: body
    });
}

export async function deleteUser(userId, deleteState) {
    const body = new FormData();
    body.append('state', deleteState);

    const response = await fetch(`https://localhost:44365/api/User/${userId}/delete`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
        body: body
    });
}