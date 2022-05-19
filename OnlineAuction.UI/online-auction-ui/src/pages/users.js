import { getUsers } from "../services/userService.js";
import { useEffect, useState } from 'react';
import UsersTable from "../components/users/usersTable";

function Users({isAuth}) {

    const [ pagination, setPagination ] = useState({
        page: 1,
        pageSize: 10
    });

    const [ users, setUsers ] = useState(null);
    const [ userTable, setUserTable ] = useState();

    useEffect(() => {
        setUserTable(<UsersTable users={users} updateUsersTable={updateUsersTable} />)
    }, [users]);

    function updateUsersTable(){
        getUsers(pagination.page, pagination.pageSize).then(val => {
            setUsers(val);
        })
    }

    if(!users && isAuth) {
        updateUsersTable();
    }

    return (
        <div>
            { userTable }
        </div>
    );
}
  
export default Users;