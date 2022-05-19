import { getUsers } from "../services/userService.js";
import { useState, useEffect } from 'react';
import UsersTable from "../components/users/usersTable";


function Users({isAuth}) {

    const [ pagination, setPagination ] = useState({
        page: 1,
        pageSize: 10
    });

    const [ users, setUsers ] = useState(null);
    const [ usersRows, setUsersRows ] = useState();

    
    if(!users && isAuth) {
        getUsers(pagination.page, pagination.pageSize).then(val => {
            setUsers(val);
        })
    }

    return (
        <div>
            Users Table
            <div>
                <UsersTable users={users} />
            </div>
        </div>
    );
}
  
export default Users;