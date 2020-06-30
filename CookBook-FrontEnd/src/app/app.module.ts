import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule }    from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { RecipeShortItemComponent } from './recipe-short-item/recipe-short-item.component';
import { RecipeFullItemComponent } from './recipe-full-item/recipe-full-item.component';
import { ModalCreateOptionsComponent } from './modal-create-options/modal-create-options.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ParentHierarchyListComponent } from './parent-hierarchy-list/parent-hierarchy-list.component';

@NgModule({
  declarations: [
    AppComponent,
    RecipeListComponent,
    RecipeShortItemComponent,
    RecipeFullItemComponent,
    ModalCreateOptionsComponent,
    ParentHierarchyListComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule, FormsModule, NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
