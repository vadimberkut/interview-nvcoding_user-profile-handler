<template>
    <div class="user-editor">
        <form>
            <div class="image-editor">
                <input v-on:change="onFileChange" type="file" name="image" />
                <div v-if="imageUrl">
                    <img v-bind:src="imageUrl" />
                </div>
                <div v-else>
                 <div class="title">User image</div>
                </div>
            </div>
            <div>
                <router-view></router-view>
                <div class="input-group">
                    <label>Name</label>
                    <input v-model="shared.editableUserProfile.name" v-on:keyup="onKeyup" type="text" name="name" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Email</label>
                    <input v-model="shared.editableUserProfile.email" v-on:keyup="onKeyup" type="email" name="email" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Skype</label>
                    <input v-model="shared.editableUserProfile.skypeLogin" v-on:keyup="onKeyup" type="text" name="skype" class="input-control" />
                </div>
                <div class="input-group">
                    <label>Signature</label>
                    <input v-model="shared.editableUserProfile.signature" v-on:keyup="onKeyup" type="text" name="signature" class="input-control" />
                </div>
            </div>
        </form>
    </div>
</template>

<script>
import _ from 'lodash';
import store from '../store.js';

export default {
    name: 'UserEdit',
    beforeRouteEnter (to, from, next) {
        console.log('beforeRouteEnter ', to, from);
        let isEdit = !!to.params.id;
        if(isEdit) {
            let profile = store.state.userProfiles.find(x => x.id === to.params.id);
            store.actions.setEditableUserProfile(profile);
        }
        else {
            store.actions.resetEditableUserProfile();
        }
        next();
    },
    beforeRouteUpdate: function(to, from, next) {
        console.log('beforeRouteUpdate ', to, from);
        let isEdit = !!to.params.id;
        if(isEdit) {
            let profile = store.state.userProfiles.find(x => x.id === to.params.id);
            if(!profile) {
                this.$router.push('/user/create');
                return;
            }
            store.actions.setEditableUserProfile(profile);
        }
        else {
            store.actions.resetEditableUserProfile();
        }
        console.log(to.params.id, this.shared.editableUserProfile.id, this.shared.editableUserProfile.name);
        next();
    },
    beforeRouteLeave (to, from, next) {
        console.log('beforeRouteLeave ', to, from);
        next();
    },
    data: function() {
        return {
            private: {

            },
            shared: store.state
        };
    },
    computed: {
        isEditMode: function() {
            return !!this.$route.params.id;
        },
        imageUrl: function() {
            let result = this.shared.editableUserProfile.imageBase64 ? 
                this.shared.editableUserProfile.imageBase64 : 
                this.shared.editableUserProfile.imageUrlAbsolute;
            return result;
        }
    },
    watch: {
        // user: {
        //     handler: function(newValue, oldValue) {
        //         // When object chaged
        //     },
        //     deep: true
        // }
    },
    created: function() {
        if(this.isEditMode) {
            // If editable user missed in store redirect to home
            let present = store.state.userProfiles.some(x => x.id === this.$route.params.id);
            if(!present) {
                console.log('Redirecting home');
                this.$router.push('/');
                return;
            }
        }

        this.validateName = function(name) {
            let re = /^[a-zA-Z ]*$/;
            return re.test(name);
        }
        this.validateEmail = function(email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(String(email).toLowerCase());
        };
        this.validate = function() {
            let valid = true;

            if(!this.shared.editableUserProfile.name || this.shared.editableUserProfile.name.length === 0 || !this.validateName(this.shared.editableUserProfile.name)) {
                valid = false;
            }
            if(!this.shared.editableUserProfile.email || this.shared.editableUserProfile.email.length === 0 || !this.validateEmail(this.shared.editableUserProfile.email)) {
                valid = false;
            }
            // if(this.user.skypeLogin && this.user.skypeLogin.length > 200) {
            //     valid = false;
            // }
            // if(this.user.signature && this.user.skypeLogin.signature > 200) {
            //     valid = false;
            // }

            return valid
        };
        this.userProfileChanged = _.debounce(this.saveUserProfile, 500);
    },
    methods: {
        onKeyup: function(e) {
            this.userProfileChanged();
        },
        onFileChange: function(e) {
            console.log(e);
            let self = this;
            let file = e.target.files[0];
            
            // Convert to base64
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function () {
                self.shared.editableUserProfile.imageBase64 = reader.result;
                self.userProfileChanged();
            };
            reader.onerror = function (err) {
                console.error(err);
            };
        },
        saveUserProfile: function() {
            if(!this.validate()) {
                return;
            }

            if(this.isEditMode) {
                console.log('Updating user');
                store.actions.updateUserProfile(this.shared.editableUserProfile).then(profile => {
                });
            }
            else {
                console.log('Creating a new user');
                store.actions.createUserProfile(this.shared.editableUserProfile).then(profile => {
                    // store.actions.setEditableUserProfile(profile);
                    // Navigate to edit page
                    this.$router.push(`/user/edit/profile/${profile.id}`);
                });
            }

            
        }
    }
}
</script>

<style lang="scss" scoped>
    .user-editor {
        display: flex;

        form {
            display: flex;
            align-items: flex-start;
        }

        .image-editor {
            display: flex;
            justify-content: center;
            align-items: center;
            position: relative;
            margin-right: 3rem;
            width: 300px;
            height: 400px;
            background-color: #d4d4d433;
            border: 1px solid #e6e7e9;
            background-position: center;
            background-repeat: no-repeat;
            background-size: auto;
            padding: 0.25rem;
            height: auto;
            min-height: 150px;
            min-width: 200px;
            max-width: 300px;

            input[type="file"] {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                opacity: 0;
                cursor: pointer;
            }

            .title {
                text-align: center;
                font-size: 1.5rem;
            }

            img {
                max-width: 100%;
            }
        }

        
    }
</style>
