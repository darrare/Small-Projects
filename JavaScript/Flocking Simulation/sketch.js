// Flocking
// Originally developed by Daniel Shiffman - https://thecodingtrain.com/CodingChallenges/124-flocking-boids.html
// Modified and optimized by Ryan Darras

const flock = [];

let alignSlider, cohesionSlider, separationSlider, maxForceSlider, maxSpeedSlider, perceptionRadiusSlider;

function setup() {
  createCanvas(640, 360);
  alignSlider = createSlider(0, 2, 1, 0.1);
  alignSlider.position(90,10);
  cohesionSlider = createSlider(0, 2, 1, 0.1);
  cohesionSlider.position(90,45);
  separationSlider = createSlider(0, 2, 1, 0.1);
  separationSlider.position(90,85);
  
  maxForceSlider = createSlider(0, 1, 0.2, 0.1);
  maxForceSlider.position(375,10);
  maxSpeedSlider = createSlider(1, 10, 5, 0.1);
  maxSpeedSlider.position(375,45);
  perceptionRadiusSlider = createSlider(0, 200, 25, 1);
  perceptionRadiusSlider.position(375,85);
  
  for (let i = 0; i < 200; i++) {
    flock.push(new Boid());
  }

}

function draw() {
  background(0);
  for (let boid of flock) {
    boid.edges();
    boid.flock(flock);
    boid.update();
    boid.show();
  }  
}