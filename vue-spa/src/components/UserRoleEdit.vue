<template>
    <div class="user-role-editor">
        <h3 class="title">User roles</h3>
        <div class="user-roles">
            <div class="input-group radio-group">
                <label v-for="(role, index) in shared.roles" v-bind:key="`role_${index}`">
                    <input v-model="private.roleId" v-bind:value="role.id" v-on:change="onRoleChange" type="radio" name="userrole" />
                    <span class="radio-check"></span>
                    {{ role.name }}
                </label>
            </div>
        </div>
    </div>
</template>

<script>
import _ from 'lodash';
import store from '../store';

export default {
    name: 'UserRoleEdit',
    data: function() {
        return {
            shared: store.state,
            private: {
                roleId: store.state.editableUserProfile.roleId
            }
        };
    },
    created: function() {
        let self = this;

        this.updateRole = _.debounce(function() {
            store.actions.updateRole({
                userProfileId: store.state.editableUserProfile.id,
                userProfileRoleId: self.private.roleId
            }).then(() => {
            }).catch(err => {
                // Revert vm to match the store
                self.private.roleId = store.state.editableUserProfile.roleId
            });
        }, 500);
    },
    methods: {
        onRoleChange: function(e) {
            this.updateRole();
        }
    }
}
</script>

<style lang="scss" scoped>
    .user-role-editor {
        .title {
            margin: 0 0 2rem 0;
        }
        .user-roles {
            .user-role {

            }
        }
    }
</style>
