import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from 'src/models/login.model';
import { $WebSocket } from 'angular2-websocket';
import { Post } from 'src/models/post.model';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public mode: String = 'list';
  public userLogged: Boolean = false;
  public userLogin: String;
  public posts: Post[] = [];
  public title: string = "Postagens";
  public form: FormGroup;
  private _url = 'http://localhost:5046/api';
  public options = {
    position: ["bottom", "right"],
    timeOut: 3000,
    lastOnBottom: true,
    maxStack: 3,
    animate: "fromBottom"
  }

  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private notificationsService: NotificationsService
  ) {

    this.form = this.fb.group({
      title: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(60),
        Validators.required
      ])],
      post: ['', Validators.compose([
        Validators.minLength(10),
        Validators.maxLength(2000),
        Validators.required
      ])],
      id: ['', null],
      login: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(100),
        Validators.required
      ])],
      password: ['', Validators.compose([
        Validators.minLength(3),
        Validators.maxLength(100),
        Validators.required
      ])]
    });

    this.getPosts();
  }

  ngOnInit() {
    localStorage.removeItem('token');
    localStorage.removeItem('userLogin');
    setTimeout(() => {
      var ws = new $WebSocket("ws://localhost:5046/ws");

      ws.onMessage(
        (msg: MessageEvent) => {
          const post:Post = JSON.parse(msg.data);
          this.notificationsService.success('Novo post criado',
            post['Title'],
            {
                timeOut: 5000,
                showProgressBar: true,
                pauseOnHover: false,
                clickToClose: false,
                maxLength: 10
            });
        },
        { autoApply: false }
      );
    }, 10000);
  }

  add() {
    const title = this.form.controls['title'].value;
    const post = this.form.controls['post'].value;
    var postJson = { text: post, title: title }

    this.http.post<Login>(`${this._url}/post`, postJson, { headers: this.setHeaderToken() }).subscribe(result => {
      alert('Postagem criada com sucesso');
      this.getPosts();
      this.clear();
      this.changeMode('list');
    }, error => {
      if (error && error.status == 401)
        alert('Login inválido');
    });
  }

  remove(post: Post) {
    this.http.delete<Login>(`${this._url}/post/${post.id}`, { headers: this.setHeaderToken() }).subscribe(result => {
      alert('Postagem excluída com sucesso');
      this.getPosts();
      this.clear();
      this.changeMode('list');
    }, error => {
      if (error && error.status == 401)
        alert('Login inválido');
    });
  }

  edit(post: Post) {
    this.form.controls['title'].setValue(post.title);
    this.form.controls['post'].setValue(post.text);
    this.form.controls['id'].setValue(post.id);
    this.changeMode('edit');
  }

  saveEdit(id: String) {
    const title = this.form.controls['title'].value;
    const post = this.form.controls['post'].value;
    var putJson = { text: post, title: title, id: id }

    this.http.patch<Login>(`${this._url}/post`, putJson, { headers: this.setHeaderToken() }).subscribe(result => {
      alert('Postagem alterada com sucesso');
      this.getPosts();
      this.clear();
      this.changeMode('list');
    }, error => {
      if (error && error.status == 401)
        alert('Login inválido');
    });
  }

  clear() {
    this.form.controls['title'].reset();
    this.form.controls['post'].reset();
    this.form.controls['login'].reset();
    this.form.controls['password'].reset();
  }

  changeMode(mode: String) {
    this.mode = mode;
  }

  getPosts() {
    this.http.get<Post[]>(`${this._url}/post`).subscribe(result => {
      this.posts = [];
      this.userLogged = this.userIsLogged();
      if (this.userIsLogged)
        this.userLogin = localStorage.getItem('userLogin');

      if (result)
        for (let index = 0; index < result.length; index++) {
          const element = result[index];
          this.posts.push(new Post(element.id, element.text, element.title));
        }
    }, error => console.error(error));
  }

  login() {
    const login = this.form.controls['login'].value;
    const password = this.form.controls['password'].value;
    var credentials = { email: login, password: password }
    this.http.post<Login>(`${this._url}/access-manager`, credentials).subscribe(result => {
      if (result && result.authenticated) {
        localStorage.setItem('token', result.accessToken.toString())
        localStorage.setItem('userLogin', login)
        this.userLogged = true;
        this.userLogin = login;
        this.form.controls['login'].reset();
        this.form.controls['password'].reset();
      }
      else
        alert('Login inválido');
    }, error => {
      if (error && error.status == 401)
        alert('Login inválido');
    });
  }

  setHeaderToken(): HttpHeaders {
    const token = localStorage.getItem('token');
    let header = new HttpHeaders();
    header = header.append('Content-Type', 'application/json');
    header = header.append('Authorization', `bearer ${token}`);

    return header;
  }

  userIsLogged(): Boolean {
    const token = localStorage.getItem('token');
    if (token) return true;
    return false;
  }
}
