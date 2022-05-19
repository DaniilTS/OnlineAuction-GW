export async function getUserPhoto() {
    const response = await fetch('https://localhost:44365/api/User/photo', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        },
    });

    return await response.json();
}
