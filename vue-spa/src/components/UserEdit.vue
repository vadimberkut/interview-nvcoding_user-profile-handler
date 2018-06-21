<template>
    <div class="user-editor">
        <form>
            <div class="image-editor">
                <input type="file" name="image" />
            </div>
            <div>
                <router-view></router-view>
                <div class="input-group">
                    <label>Name</label>
                    <input type="text" name="name" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Email</label>
                    <input type="email" name="email" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Skype</label>
                    <input type="text" name="skype" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Signature</label>
                    <input type="text" name="signature" class="input-control" />
                </div>
            </div>
        </form>
    </div>
</template>

<script>
export default {
    name: 'UserEdit',

}
</script>

<style lang="scss" scoped>
    .user-editor {
        display: flex;

        form {
            display: flex;
        }

        .image-editor {
            position: relative;
            margin-right: 3rem;
            width: 300px;
            height: 400px;
            background-color: rgb(212, 212, 212);
            border: 1px solid #e6e7e9;

            input[type="file"] {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                opacity: 0;
                cursor: pointer;
            }
        }

        
    }
</style>
