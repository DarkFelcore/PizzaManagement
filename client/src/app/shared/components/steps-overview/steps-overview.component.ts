import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IStep } from '../../types/recipe';

@Component({
  selector: 'app-steps-overview',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './steps-overview.component.html',
  styleUrl: './steps-overview.component.scss'
})
export class StepsOverviewComponent {
  @Input() steps!: IStep[];
  @Input() isEditable: boolean = false;

  removeStep(index: number): void {
    this.steps.splice(index, 1);
    this.calculateStepNumber();
  }

  getColumnWidth(): string {
    return this.isEditable ? "col-md-9 d-flex align-items-start" : "col-md-12 d-flex align-items-start"
  }

  private calculateStepNumber(): void {
    this.steps.forEach((item, idx) => item.number = idx + 1)
  }
}
