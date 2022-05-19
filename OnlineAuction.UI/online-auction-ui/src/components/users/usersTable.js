
function UsersTable(props) {

    console.log(props);
    const usersRows = props.users?.map(user => {
        return <p>{user.email} {user.phone} {user.isDeleted.value} {user.isBlocked} {user.created}</p>
    });

    return (
        <div>
            {usersRows}
        </div>
    );
}
  
export default UsersTable;