<template>
    <div class="user-role-editor">
        <h3 class="title">User roles</h3>
        <div class="user-roles">
            <div class="input-group radio-group">
                <label>
                    <input type="radio" name="userrole" value="1" />
                    <span class="radio-check"></span>
                    User
                </label>
                <label>
                    <input type="radio" name="userrole" value="2" />
                    <span class="radio-check"></span>
                    User
                </label>
                <label>
                    <input type="radio" name="userrole" value="3" />
                    <span class="radio-check"></span>
                    User
                </label>
                <label>
                    <input type="radio" name="userrole" value="4" />
                    <span class="radio-check"></span>
                    User
                </label>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Edit',

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
