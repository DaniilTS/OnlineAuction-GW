
import '../../styles/usersTable.scss';
import { blockUser, deleteUser } from '../../services/userService.js';

function UsersTable(props) {
    async function setUserBlockState(id, isBlocked) {
        blockUser(id, isBlocked ? false : true).then(val => {
            props.updateUsersTable();
        });
    }

    async function setUserDeleteState(id, isDeleted) {
        deleteUser(id, isDeleted ? false : true).then(val => {
            props.updateUsersTable();
        });
    }

    const usersRows = props.users?.map((user, index) => {
        return (<tr className='users__row' key={user.id}>
                    <td className='row-index'>{index + 1}</td>
                    <td className='row-property'>{user.email}</td>
                    <td className='row-property'>{user.phone}</td>
                    <td className='row-property'>{user.isBlocked.toString()}</td>
                    <td className='row-property'>{user.isDeleted.toString()}</td>
                    <td className='row-property'>{user.created.substring(0, 10)}</td>
                    <td className='row-actions' colSpan={2}>
                        <button className='actions__button button-block' onClick={() => setUserBlockState(user.id, user.isBlocked)} >{user.isBlocked ? 'Unblock' : 'Block'}</button>
                        <button className='actions__button button-delete' onClick={() => setUserDeleteState(user.id, user.isDeleted)} >{user.isDeleted ? 'Undelete' : 'Delete'}</button>
                    </td>
               </tr>)
    });

    return (
        <table className='users__table' cellSpacing={0} cellPadding={0}>
            <thead className='table__head'>
                <tr>
                    <th className='table__cell'>№</th>
                    <th className='table__cell'>Email</th>
                    <th className='table__cell'>Phone</th>
                    <th className='table__cell'>Is Blocked</th>
                    <th className='table__cell'>Is Deleted</th>
                    <th className='table__cell'>Created</th>
                    <th className='table__cell' colSpan={2}>Actions</th>
                </tr>
            </thead>
            <tbody className='table__body'>
                {usersRows}
            </tbody>
            
        </table>
    );
}
  
export default UsersTable;