import { Component } from '@angular/core';
import { GestFormService } from './services/gestform-services';
import { FormControl, Validators } from '@angular/forms';
import { ItemQuery } from './models/item-query';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  items: ItemQuery[] = [];

  estDispo: boolean = false;
  longueur = new FormControl(1, [Validators.required, Validators.min(1), Validators.max(1000)]);

  constructor(
    private gestFormService: GestFormService) {
  }


  afficherMessageErreur() {
    this.estDispo = false;
    return "Veuillez entrer un longueur valide"
  }


  lancerGetFormTest(n: number) {
    this.estDispo = false;

    var listeNombres = [];

    for (var i = 1; i <= n; i++)
      listeNombres.push(this.genererNombreAleatoireEntreMinEtMax(-1000, 1000));


    this.gestFormService.testerListe(listeNombres).subscribe(resultat => {
      this.items = resultat;
      this.estDispo = true;
    }
    );
  }
  
  private genererNombreAleatoireEntreMinEtMax(min: number, max: number) {
    return Math.floor(Math.random() * (max - min)) + min;
  }

}
